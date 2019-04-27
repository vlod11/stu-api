using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.Helpers.Mappers;

namespace UniHub.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class UserController : BaseController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly IUserService _usersService;

        public UserController(
            IServiceResultMapper viewMapper,
            IUserService userService)
        {
            _viewMapper = viewMapper;
            _usersService = userService;
        }

        [HttpGet("{usersProfileId}")]
        public async Task<IActionResult> GetUsersProfileAsync([FromRoute] int userId)
        => _viewMapper.ServiceResultToContentResult(
                await _usersService.GetUserAsync(userId));
    }
}