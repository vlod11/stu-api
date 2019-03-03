using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;

namespace UniHub.WebApi.BLL.Services
{
    public interface IAuthorizationService
    {
         Task<ServiceResult<object>> LoginAsync(LoginUserRequest request);
         Task<ServiceResult<object>> RegisterStudentAsync(RegisterUserRequest request);
         Task<ServiceResult<object>> ConfirmPasswordAsync(string username, string email);
    }
}