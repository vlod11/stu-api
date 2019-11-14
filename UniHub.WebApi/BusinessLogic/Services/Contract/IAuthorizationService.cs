using System.Threading.Tasks;
using UniHub.WebApi.Common.Token;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Models;
using UniHub.WebApi.Models.Requests.Authorization;

namespace UniHub.WebApi.BusinessLogic.Services.Contract
{
    public interface IAuthorizationService
    {
         Task<ServiceResult<AuthDto>> LoginAsync(LoginUserRequest request);
         Task<ServiceResult<object>> RegisterStudentAsync(RegisterUserRequest request);
         Task<ServiceResult<string>> ConfirmEmailAsync(string email);
         Task<ServiceResult<TokenModel>> UpdateTokenAsync(RefreshTokenRequest refreshToken);
    }
}