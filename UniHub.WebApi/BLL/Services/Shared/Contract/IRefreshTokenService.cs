using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniHub.WebApi.BLL.Services.Contract
{
    public interface IRefreshTokenService
    {
        Task<bool> IsValidRefreshTokenAsync(string refreshToken);
        Task<TokenModel> GetTokenModelAsync(IEnumerable<KeyValuePair<object, object>> keyValues, int userId, string oldRefreshToken = "");
    }
}