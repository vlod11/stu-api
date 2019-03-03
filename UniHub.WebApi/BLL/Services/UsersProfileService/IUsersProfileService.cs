using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;

namespace UniHub.WebApi.BLL.Services
{
    public interface IUsersProfileService
    {
         Task<ServiceResult<UsersProfileDto>> GetUsersProfileAsync(int usersProfileId);
    }
}