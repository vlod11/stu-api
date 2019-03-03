using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniHub.WebApi.Shared.Token
{
    public interface ITokenService
    {
        AccessTokenModel GetTokenModel(IEnumerable<KeyValuePair<object, object>> keyValues);
    }
}