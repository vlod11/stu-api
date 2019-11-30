using System.Threading.Tasks;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;
using UniHub.Model.Request.UserProfile;

namespace UniHub.Services.Contract
{
    public interface IUserService
    {
         Task<ServiceResult<UserDto>> GetUserAsync(int userId);
         Task<ServiceResult<UserDto>> UpdatePasswordAsync(int userId, UpdatePasswordRequest request);
         Task<ServiceResult<UserDto>> UpdateUsersInfoAsync(int userId, UpdateUserInfoRequest request);
    }
}