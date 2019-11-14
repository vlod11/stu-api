using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BusinessLogic.Services.Contract;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Requests.UserProfile;
using UniHub.WebApi.Web.Helpers.Mappers;

namespace UniHub.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class SettingsController : BaseController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly IUserService _userService;

        public SettingsController(
            IServiceResultMapper viewMapper,
            IUserService userService)
        {
            _viewMapper = viewMapper;
            _userService = userService;
        }

        /// <summary>
        /// Update users information. If field isn't changed, leave it empty
        /// </summary>
        [HttpPut("Info")]
        [Authorize]
        public async Task<ActionResult<UserDto>> UpdateUserInfoAsync([FromBody] UpdateUserInfoRequest request)
            => _viewMapper.ServiceResultToContentResult
                (await _userService.UpdateUsersInfoAsync(UserId, request));

        /// <summary>
        /// Update users password
        /// </summary>
        [HttpPut("Password")]
        [Authorize]
        public async Task<ActionResult<UserDto>> UpdateUserPasswordAsync([FromBody] UpdatePasswordRequest request)
            => _viewMapper.ServiceResultToContentResult
                (await _userService.UpdatePasswordAsync(UserId, request));
    }
}