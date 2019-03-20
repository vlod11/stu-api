using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.ModelLayer.Requests.UserProfile;

namespace UniHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : UserBaseController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly IUsersProfileService _usersProfileService;

        public SettingsController(
            IServiceResultMapper viewMapper,
            IUsersProfileService usersProfileService)
        {
            _viewMapper = viewMapper;
            _usersProfileService = usersProfileService;
        }

        /// <summary>
        /// Update users information. If field isn't changed, leave it empty
        /// </summary>
        [HttpPut("Info")]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfoAsync([FromBody] UpdateUserInfoRequest request)
            => _viewMapper.ServiceResultToContentResult
                (await _usersProfileService.UpdateUsersInfoAsync(UserId, request));

        /// <summary>
        /// Update users password
        /// </summary>
        [HttpPut("Password")]
        [Authorize]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] UpdatePasswordRequest request)
            => _viewMapper.ServiceResultToContentResult
                (await _usersProfileService.UpdatePasswordAsync(UserId, request));
    }
}