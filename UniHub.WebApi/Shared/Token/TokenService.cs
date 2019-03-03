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
using UniHub.WebApi.Shared.Options;

namespace UniHub.WebApi.Shared.Token
{
    public class TokenService : ITokenService
    {
        private readonly TokenInfo _tokenInfo;

        public TokenService(IOptions<TokenOptions> tokenOptions)
        {
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

        public AccessTokenModel GetTokenModel(IEnumerable<KeyValuePair<object, object>> keyValues)
        {
            return BuildAccessToken(keyValues.ToLookup(x => x.Key, x => x.Value));
        }

        private AccessTokenModel BuildAccessToken(ILookup<object, object> claimsLookUp)
        {
            var now = DateTime.UtcNow;
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

            return new AccessTokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpireAt = expires
            };
        }
    }
}