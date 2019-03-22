using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.Helpers.Mappers;

namespace UniHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : UserBaseController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly IPostService _postService;

        public ProfileController(
            IServiceResultMapper viewMapper,
            IPostService postService)
        {
            _viewMapper = viewMapper;
            _postService = postService;
        }

        [HttpGet("Posts")]
        [Authorize]
        public async Task<IActionResult> GetMyPostsAsync(int skip = 0, int take = 0)
        => _viewMapper.ServiceResultToContentResult(
                await _postService.GetUsersPostsAsync(UserId, skip, take));
    }
}