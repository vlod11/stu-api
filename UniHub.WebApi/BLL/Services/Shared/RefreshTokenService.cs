using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UniHub.WebApi.BLL.Helpers.Contract;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.Common.Options;
using UniHub.WebApi.Common.Token;

namespace UniHub.WebApi.BLL.Services.Shared
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<TokenOptions> _tokenOptions;
        private readonly ITokenService _tokenService;
        private readonly IDateHelper _dateHelper;

        public RefreshTokenService(
            IUnitOfWork unitOfWork,
            IOptions<TokenOptions> jwtTokenOptions,
            ITokenService tokenProvider,
            IDateHelper dateHelper)
        {
            _dateHelper = dateHelper;
            _unitOfWork = unitOfWork;
            _tokenOptions = jwtTokenOptions;
            _tokenService = tokenProvider;
        }

        public async Task<TokenModel> GetTokenModelAsync(IEnumerable<KeyValuePair<object, object>> keyValues, int userId,
            string oldRefreshToken = "")
        {
            var accessToken = _tokenService.GetTokenModel(keyValues);
            string refreshToken = GetRefreshToken();

            await PutRefreshTokenAsync(userId, refreshToken, oldRefreshToken);

            return new TokenModel
            {
                RefreshToken = refreshToken,
                AccessToken = accessToken.AccessToken,
                ExpireAt = accessToken.ExpireAt
            };
        }

        public async Task<bool> IsValidRefreshTokenAsync(string refreshToken)
        {
            return await _unitOfWork.RefreshTokenRepository.AnyAsync(x =>
                x.Token == refreshToken && x.ExpiredAt >= _dateHelper.GetDateTimeUtcNow());
        }

        private string GetRefreshToken()
        {
            var bytesArray = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytesArray);
                return Convert.ToBase64String(bytesArray);
            }
        }

        private async Task PutRefreshTokenAsync(int userId, string refreshToken, string oldRefreshToken = "")
        {
            var dbRefreshToken = await _unitOfWork.RefreshTokenRepository.GetSingleAsync(x => x.UserId == userId);
            if (dbRefreshToken == null)
            {
                var refreshTokenEntity = new RefreshToken
                {
                    UserId = userId,
                    Token = refreshToken,
                    ExpiredAt = _dateHelper.GetDateTimeUtcNow().AddMinutes(_tokenOptions.Value.RefreshTokenLifeTime),
                    CreatedAt = _dateHelper.GetDateTimeUtcNow(),
                    ModifiedAt = _dateHelper.GetDateTimeUtcNow()
                };
                await _unitOfWork.RefreshTokenRepository.AddAsync(refreshTokenEntity);
            }
            else
            {
                dbRefreshToken.Token = refreshToken;
                dbRefreshToken.ExpiredAt = _dateHelper.GetDateTimeUtcNow().AddMinutes(_tokenOptions.Value.RefreshTokenLifeTime);
                dbRefreshToken.ModifiedAt = _dateHelper.GetDateTimeUtcNow();
            }

            await _unitOfWork.CommitAsync();
        }
    }
}