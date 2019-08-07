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
using Serilog;
using UniHub.WebApi.BLL.Helpers.Contract;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.BLL.Services.Shared.Contract;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.Helpers;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Enums;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.Common.Options;
using UniHub.WebApi.Common.Token;

namespace UniHub.WebApi.BLL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly UrlsOptions _urlOptions;
        private readonly ILogger<AuthorizationService> _logger;
        private readonly IDateHelper _dateHelper;
        private readonly IEncryptHelper _encryptHelper;

        public AuthorizationService(
            IUnitOfWork repositoryWrapper,
            IMapper mapper,
            IEmailService emailService,
            IRefreshTokenService refreshTokenService,
            IOptions<UrlsOptions> urlOptions,
            ILogger<AuthorizationService> logger,
            IDateHelper dateHelper,
            IEncryptHelper encryptHelper)
        {
            _encryptHelper = encryptHelper;
            _dateHelper = dateHelper;
            _emailService = emailService;
            _mapper = mapper;
            _unitOfWork = repositoryWrapper;
            _refreshTokenService = refreshTokenService;
            _urlOptions = urlOptions.Value;
            _logger = logger;
        }

        public async Task<ServiceResult<AuthDto>> LoginAsync(LoginUserRequest request)
        {
            _logger.LogInformation("Loooooooooog1 " + request.Email);

            // Log level hierarchy: 
            // Trace, Debug, Information, Warning, Error, Critical
            _logger.LogTrace("IndexModel.OnGet entered (trace)");
            _logger.LogDebug("IndexModel.OnGet entered (debug)");
            _logger.LogInformation("IndexModel.OnGet entered (info)");
            _logger.LogWarning("IndexModel.OnGet entered (warn)");
            _logger.LogError("IndexModel.OnGet entered (error)");
            _logger.LogCritical("IndexModel.OnGet entered (crit)");


            //find user
            var userInfo = await _unitOfWork.UserRepository.GetUserAsync(request.Email, true);

            if (userInfo == null || !Authenticate.Verify(request.Password, userInfo.PasswordHash))
            {
                return ServiceResult<AuthDto>.Fail(EOperationResult.ValidationError,
                    "Email or password is incorrect.");
            }

            return ServiceResult<AuthDto>.Ok(await GenerateToken(userInfo));
        }

        public async Task<ServiceResult<AuthDto>> RegisterStudentAsync(RegisterUserRequest request)
        {
            if (await _unitOfWork.UserRepository.AnyAsync(u => u.Email.ToUpperInvariant() == request.Email.ToUpperInvariant()))
            {
                return ServiceResult<AuthDto>.Fail(EOperationResult.AlreadyExist, "User with this email already exist");
            }

            if (await _unitOfWork.UserRepository.IsUserExistByUsernameAsync(request.Username))
            {
                return ServiceResult<AuthDto>.Fail(EOperationResult.AlreadyExist, "User with this username already exist");
            }

            string encryptedString = _encryptHelper.Encrypt(request.Email);

            var confirmationUrl = $"{_urlOptions.AppUrl}/Authorization/emailConfirmation/{HttpUtility.UrlEncode(encryptedString)}";

            var sendEmailResult =
            await _emailService.SendUserValidationEmailAsync(request.Email, request.Username, confirmationUrl);

            if (!sendEmailResult.IsSuccess)
            {
                return ServiceResult<AuthDto>.Fail(EOperationResult.SendEmailError,
                    $"Error while sending email with status code: {sendEmailResult.Result}.");
            }

            var user = new User()
            {
                Email = request.Email,
                CreatedAt = _dateHelper.GetDateTimeUtcNow(),
                PasswordHash = Authenticate.Hash(request.Password),
                Username = request.Username,
                RoleId = (int)ERoleType.UnverifiedStudent,
                Avatar = Constants.DefaultImage
            };

            _unitOfWork.UserRepository.Create(user);
            await _unitOfWork.CommitAsync();

            return ServiceResult<AuthDto>.Ok(await GenerateToken(user));
        }

        public async Task<ServiceResult<string>> ConfirmEmailAsync(string emailToken)
        {
            _encryptHelper.TryDecrypt(emailToken, out string email);

            var user = await _unitOfWork.UserRepository.GetSingleAsync(u => u.Email.ToUpperInvariant() == email.ToUpperInvariant());

            if (user == null)
            {
                return ServiceResult<string>.Fail(EOperationResult.EntityNotFound,
                    "Invalid email verification token.");
            }

            user.IsValidated = true;
            user.ModifiedAt = _dateHelper.GetDateTimeUtcNow();

            await _unitOfWork.CommitAsync();

            return ServiceResult<string>.Ok(email);
        }

        public async Task<ServiceResult<TokenModel>> UpdateTokenAsync(RefreshTokenRequest refreshToken)
        {
            var oldRefreshToken = (await _unitOfWork.RefreshTokenRepository.GetSingleAsync(r => r.Token == refreshToken.RefreshToken, r => r.User));
            if (oldRefreshToken?.User == null)
            {
                return ServiceResult<TokenModel>.Fail(EOperationResult.ValidationError, "Refresh token is invalid");
            }

            var isRefreshTokenValid =
                await _refreshTokenService.IsValidRefreshTokenAsync(refreshToken.RefreshToken);
            if (!isRefreshTokenValid)
            {
                return ServiceResult<TokenModel>.Fail(EOperationResult.ValidationError, "Refresh token is invalid");
            }

            List<KeyValuePair<object, object>> keyValueList = new List<KeyValuePair<object, object>>
            {
                    new KeyValuePair<object, object>(SetOfKeysForClaims.UserId, oldRefreshToken.User.Id)
            };

            return ServiceResult<TokenModel>.Ok(
                await _refreshTokenService.GetTokenModelAsync(keyValueList, oldRefreshToken.UserId, refreshToken.RefreshToken));
        }

        private async Task<AuthDto> GenerateToken(User user)
        {
            List<KeyValuePair<object, object>> keyValueList = new List<KeyValuePair<object, object>>
                {
                    new KeyValuePair<object, object>(SetOfKeysForClaims.UserId, user.Id),
                    new KeyValuePair<object, object>(ClaimTypes.Role, (ERoleType)user.RoleId)
                };

            TokenModel tokenModel = await _refreshTokenService.GetTokenModelAsync(keyValueList, user.Id);

            return new AuthDto()
            {
                AccessToken = tokenModel.AccessToken,
                RefreshToken = tokenModel.RefreshToken,
                ExpireAt = tokenModel.ExpireAt,
                User = _mapper.Map<User, UserDto>(user)
            };
        }
    }
}