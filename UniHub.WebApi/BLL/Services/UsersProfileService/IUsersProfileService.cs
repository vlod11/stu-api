using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.ModelLayer.Requests.UserProfile;

namespace UniHub.WebApi.BLL.Services
{
    public interface IUsersProfileService
    {
         Task<ServiceResult<UsersProfileDto>> GetUsersProfileAsync(int usersProfileId);
         Task<ServiceResult<UsersProfileDto>> UpdatePasswordAsync(int usersProfileId, UpdatePasswordRequest request);
         Task<ServiceResult<UsersProfileDto>> UpdateUsersInfoAsync(int usersProfileId, UpdateUserInfoRequest request);
    }
}