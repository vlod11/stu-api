using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.Helpers.Mappers;

namespace UniHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersProfilesController : UserBaseController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly IUsersProfileService _usersProfileService;

        public UsersProfilesController(
            IServiceResultMapper viewMapper,
            IUsersProfileService usersProfileService)
        {
            _viewMapper = viewMapper;
            _usersProfileService = usersProfileService;
        }

        [HttpGet("{usersProfileId}")]
        public async Task<IActionResult> GetUsersProfileAsync([FromRoute] int usersProfileId)
        => _viewMapper.ServiceResultToContentResult(
                await _usersProfileService.GetUsersProfileAsync(usersProfileId));
    }
}