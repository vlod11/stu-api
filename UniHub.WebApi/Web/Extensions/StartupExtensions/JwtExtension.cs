using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UniHub.WebApi.DataAccess.RepositoryService;
using UniHub.WebApi.Shared.Options;
using UniHub.WebApi.Shared.Token;

namespace UniHub.WebApi.Extensions
{
    public static class JwtExtension
    {
        public static void AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Shared.Options.TokenOptions>(configuration.GetSection("Token"));
            var tokenOptions = configuration.GetSection("Token").Get<Shared.Options.TokenOptions>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = tokenOptions.ValidateIssuer,
                        ValidateLifetime = tokenOptions.ValidateLifetime,
                        ValidateIssuerSigningKey = tokenOptions.ValidateIssuerSigningKey,
                        ValidateAudience = tokenOptions.ValidateAudience,
                        ClockSkew = TimeSpan.Zero,

                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenOptions.IssuerSecurityKey))
                    };
                });
        }
    }
}