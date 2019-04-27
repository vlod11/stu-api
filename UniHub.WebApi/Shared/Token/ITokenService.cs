using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniHub.WebApi.Shared.Token
{
    public interface ITokenService
    {
        TokenModel GetTokenModel(IEnumerable<KeyValuePair<object, object>> keyValues);
    }
}