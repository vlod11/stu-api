using System.Collections.Generic;

namespace UniHub.Common.Token
{
    public interface ITokenService
    {
        TokenModel GetTokenModel(IEnumerable<KeyValuePair<object, object>> keyValues);
    }
}