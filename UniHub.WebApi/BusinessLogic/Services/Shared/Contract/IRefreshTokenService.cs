using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.Common.Token;

namespace UniHub.WebApi.BusinessLogic.Services.Shared.Contract
{
    public interface IRefreshTokenService
    {
        Task<bool> IsValidRefreshTokenAsync(string refreshToken);
        Task<TokenModel> GetTokenModelAsync(IEnumerable<KeyValuePair<object, object>> keyValues, int userId, string oldRefreshToken = "");
    }
}