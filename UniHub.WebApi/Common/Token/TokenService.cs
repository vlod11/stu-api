using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UniHub.WebApi.BusinessLogic.Helpers.Contract;
using UniHub.WebApi.Common.Options;

namespace UniHub.WebApi.Common.Token
{
    public class TokenService : ITokenService
    {
        private readonly TokenInfo _tokenInfo;
        private readonly IDateHelper _dateHelper;

        public TokenService(IOptions<TokenOptions> tokenOptions,
                            IDateHelper dateHelper)
        {
            _dateHelper = dateHelper;

            _tokenInfo = new TokenInfo
            {
                Audience = tokenOptions.Value.Audience,
                Issuer = tokenOptions.Value.Issuer,
                IssuerSecurityKey = tokenOptions.Value.IssuerSecurityKey,
                Lifetime = tokenOptions.Value.AccessTokenLifetime,

                ValidateAudience = tokenOptions.Value.ValidateAudience,
                ValidateIssuer = tokenOptions.Value.ValidateIssuer,
                ValidateLifetime = tokenOptions.Value.ValidateLifetime,
                ValidateIssuerSigningKey = tokenOptions.Value.ValidateIssuerSigningKey
            };
        }

        public TokenModel GetTokenModel(IEnumerable<KeyValuePair<object, object>> keyValues)
        {
            return BuildAccessToken(keyValues.ToLookup(x => x.Key, x => x.Value));
        }

        private TokenModel BuildAccessToken(ILookup<object, object> claimsLookUp)
        {
            var now = _dateHelper.GetDateTimeUtcNow();
            var expires = now.AddMinutes(_tokenInfo.Lifetime);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenInfo.Issuer,
                audience: _tokenInfo.Audience,
                notBefore: now,
                claims: claimsLookUp.Any()
                    ? claimsLookUp.Select(x => new Claim(x.Key.ToString(), x.FirstOrDefault()?.ToString()))
                    : null,
                expires: expires,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenInfo.IssuerSecurityKey)),
                    SecurityAlgorithms.HmacSha256));

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpireAt = expires
            };
        }
    }
}