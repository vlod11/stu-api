using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.ModelLayer.Requests;

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
        /// Update users info
        /// </summary>
        /// </remarks>
        /// <param name="request">UpdateUserRequest</param>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfoAsync([FromBody] UpdateUserRequest request)
            => _viewMapper.ServiceResultToContentResult
                (await _usersProfileService.UpdateUsersInfo(UserId, request));
    }
}