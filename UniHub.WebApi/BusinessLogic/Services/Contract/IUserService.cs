using System.Threading.Tasks;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Models;
using UniHub.WebApi.Models.Requests.UserProfile;

namespace UniHub.WebApi.BusinessLogic.Services.Contract
{
    public interface IUserService
    {
         Task<ServiceResult<UserDto>> GetUserAsync(int userId);
         Task<ServiceResult<UserDto>> UpdatePasswordAsync(int userId, UpdatePasswordRequest request);
         Task<ServiceResult<UserDto>> UpdateUsersInfoAsync(int userId, UpdateUserInfoRequest request);
    }
}