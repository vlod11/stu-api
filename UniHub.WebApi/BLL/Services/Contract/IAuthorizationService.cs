using System.Threading.Tasks;
using UniHub.WebApi.Common.Token;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;

namespace UniHub.WebApi.BLL.Services.Contract
{
    public interface IAuthorizationService
    {
         Task<ServiceResult<AuthDto>> LoginAsync(LoginUserRequest request);
         Task<ServiceResult<AuthDto>> RegisterStudentAsync(RegisterUserRequest request);
         Task<ServiceResult<string>> ConfirmEmailAsync(string email);
         Task<ServiceResult<TokenModel>> UpdateTokenAsync(RefreshTokenRequest refreshToken);
    }
}