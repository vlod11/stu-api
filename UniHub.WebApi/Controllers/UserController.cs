using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.ModelLayer.ModelDto;

namespace UniHub.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{api-version:apiVersion}/[controller]")]
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

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUsersProfileAsync([FromRoute] int userId)
        => _viewMapper.ServiceResultToContentResult(
                await _usersService.GetUserAsync(userId));
    }
}