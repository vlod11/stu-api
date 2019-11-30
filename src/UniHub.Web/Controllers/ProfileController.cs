using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.Model.Read.ModelDto;
using UniHub.Services.Contract;
using UniHub.Web.Helpers.Mappers;

namespace UniHub.Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class ProfileController : BaseController
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
        public async Task<ActionResult<IEnumerable<PostProfileDto>>> GetMyPostsAsync(int skip = 0, int take = 0)
        => _viewMapper.ServiceResultToContentResult(
                await _postService.GetUsersPostsAsync(UserId, skip, take));
    }
}