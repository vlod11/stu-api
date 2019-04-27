using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.ModelLayer.Requests.User;

namespace UniHub.WebApi.Controllers
{
    [Route("[controller]")]
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
        public async Task<IActionResult> UpdateUserInfoAsync([FromBody] UpdateUserInfoRequest request)
            => _viewMapper.ServiceResultToContentResult
                (await _userService.UpdateUsersInfoAsync(UserId, request));

        /// <summary>
        /// Update users password
        /// </summary>
        [HttpPut("Password")]
        [Authorize]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] UpdatePasswordRequest request)
            => _viewMapper.ServiceResultToContentResult
                (await _userService.UpdatePasswordAsync(UserId, request));
    }
}