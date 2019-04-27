using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.ModelLayer.Requests.User;

namespace UniHub.WebApi.BLL.Services.Contract
{
    public interface IUserService
    {
         Task<ServiceResult<UserDto>> GetUserAsync(int userId);
         Task<ServiceResult<UserDto>> UpdatePasswordAsync(int userId, UpdatePasswordRequest request);
         Task<ServiceResult<UserDto>> UpdateUsersInfoAsync(int userId, UpdateUserInfoRequest request);
    }
}