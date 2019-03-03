using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using FluentEmail.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.Helpers;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Enums;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.Shared.Options;
using UniHub.WebApi.Shared.Token;

namespace UniHub.WebApi.BLL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly UrlsOptions _urlOptions;

        public AuthorizationService(
            IUnitOfWork repositoryWrapper,
            IMapper mapper,
            IEmailService emailService,
            ITokenService tokenService,
            IOptions<UrlsOptions> urlOptions)
        {
            _emailService = emailService;
            _mapper = mapper;
            _unitOfWork = repositoryWrapper;
            _tokenService = tokenService;
            _urlOptions = urlOptions.Value;
        }

        public async Task<ServiceResult<object>> LoginAsync(LoginUserRequest request)
            {
                //find user
                var userInfo = await _unitOfWork.UsersProfileRepository.GetUserWithCredentials(request.Email, true);

                if (userInfo == null || !Authenticate.Verify(request.Password, userInfo.Credentional.PasswordHash))
                {
                    return ServiceResult<object>.Fail(EOperationResult.ValidationError,
                        "Email or password is incorrect.");
                }

                return ServiceResult<object>.Ok(await GenerateToken(userInfo.Credentional, userInfo));
            }

        public async Task<ServiceResult<object>> RegisterStudentAsync(RegisterUserRequest request)
            {
                if (await _unitOfWork.CredentionalRepository.IsUserExistByEmail(request.Email))
                {
                    return ServiceResult<object>.Fail(EOperationResult.AlreadyExist, "User with this email already exist");
                }

                if (await _unitOfWork.UsersProfileRepository.IsUserExistByUsername(request.Username))
                {
                    return ServiceResult<object>.Fail(EOperationResult.AlreadyExist, "User with this username already exist");
                }

                // Creating a registration confirmation token and sending this email to user
                string token = await GenerateEmailConfirmationTokenAsync(request);

                // TODO: Replace APIRoutes that will contain the static routes to use
                var confirmationUrl = $"{_urlOptions.AppUrl}/api/authorization/emailConfirmation/{HttpUtility.UrlEncode(request.Username)}/{HttpUtility.UrlEncode(token)}";

                ServiceResult<SendResponse> sendEmailResult =
                await _emailService.UserRegistrationAsync(request.Email, request.Username, confirmationUrl);

                if (!sendEmailResult.IsSuccess)
                {
                    return ServiceResult<object>.Fail(EOperationResult.SendEmailError,
                        $"Error while sending email with status code: {sendEmailResult.Result}.");
                }

                var credentional = new Credentional()
                {
                    Email = request.Email,
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = Authenticate.Hash(request.Password),
                };

                var profile = new UsersProfile()
                {
                    Username = request.Username,
                    RoleId = (int)ERoleType.UnverifiedStudent,
                    Credentional = credentional,
                    Avatar = Constants.DefaultImage
                };

                //save changes
                _unitOfWork.CredentionalRepository.Create(credentional);
                _unitOfWork.UsersProfileRepository.Create(profile);
                await _unitOfWork.CommitAsync();

                return ServiceResult<object>.Ok(await GenerateToken(credentional, profile));
            }

        public async Task<ServiceResult<object>> ConfirmPasswordAsync(string username, string emailToken)
        {
            var tokenString = new JwtSecurityTokenHandler().ReadToken(emailToken) as JwtSecurityToken;

            var email = tokenString.Claims.First(claim => claim.Type == SetOfKeysForClaims.EmailClaimKey).Value;

            //find user
            var userInfo = await _unitOfWork.UsersProfileRepository.GetUserWithCredentials(email, true);

            if (userInfo == null)
            {
                return ServiceResult<object>.Fail(EOperationResult.EntityNotFound,
                    "Invalid Email Verification Token.");
            }

            if (userInfo.Username != username)
            {
                return ServiceResult<object>.Fail(EOperationResult.ValidationError,
                    "Invalid Email Verification Token.");
            }

            userInfo.RoleId = (int)ERoleType.Student;

            _unitOfWork.UsersProfileRepository.Update(userInfo);
            await _unitOfWork.CommitAsync();

            return ServiceResult<object>.Ok();
        }

        private async Task<string> GenerateEmailConfirmationTokenAsync(RegisterUserRequest request)
        {
            List<KeyValuePair<object, object>> keyValueList = new List<KeyValuePair<object, object>>
            {
                new KeyValuePair<object, object>(SetOfKeysForClaims.EmailClaimKey, request.Email)
            };

            AccessTokenModel accessTokenModel = _tokenService.GetTokenModel(keyValueList);

            return accessTokenModel.AccessToken;
        }

        private async Task<object> GenerateToken(Credentional credentional, UsersProfile profile)
        {
            List<KeyValuePair<object, object>> keyValueList = new List<KeyValuePair<object, object>>
                {
                    new KeyValuePair<object, object>(SetOfKeysForClaims.UserId, profile.Id),
                    new KeyValuePair<object, object>(ClaimTypes.Role, (ERoleType)profile.RoleId),
                    new KeyValuePair<object, object>(SetOfKeysForClaims.EmailClaimKey, credentional.Email),
                    new KeyValuePair<object, object>(SetOfKeysForClaims.Username, profile.Username)
                };

            AccessTokenModel accessTokenModel = _tokenService.GetTokenModel(keyValueList);

            return new
            {
                AccessToken = accessTokenModel.AccessToken,
                UserProfile = _mapper.Map<UsersProfile, UsersProfileDto>(profile)
            };
        }
    }
}