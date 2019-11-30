using System.Threading.Tasks;
using UniHub.Common.Token;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;
using UniHub.Model.Request.Authorization;

namespace UniHub.Services.Contract
{
    public interface IAuthorizationService
    {
         Task<ServiceResult<AuthDto>> LoginAsync(LoginUserRequest request);
         Task<ServiceResult<object>> RegisterStudentAsync(RegisterUserRequest request);
         Task<ServiceResult<string>> ConfirmEmailAsync(string email);
         Task<ServiceResult<TokenModel>> UpdateTokenAsync(RefreshTokenRequest refreshToken);
    }
}