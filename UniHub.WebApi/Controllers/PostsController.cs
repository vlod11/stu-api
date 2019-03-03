using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Enums;
using UniHub.WebApi.ModelLayer.Requests;

namespace UniHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : UserBaseController
    {
        private readonly IServiceResultMapper _viewMapper;
        private readonly IPostService _postService;

        public PostsController(
            IServiceResultMapper viewMapper,
            IPostService postService)
        {
            _viewMapper = viewMapper;
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostCardsAsync(int subjectId, int skip = 0, int take = 10)
        => _viewMapper.ServiceResultToContentResult(
                await _postService.GetListOfPostCardsAsync(subjectId, skip, take));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostFullInfoAsync([FromRoute] int id)
        => _viewMapper.ServiceResultToContentResult(
                await _postService.GetPostFullInfoAsync(id));

        [HttpPost]
        [Authorize(Roles = nameof(ERoleType.Admin) + ", " + nameof(ERoleType.Student))]
        public async Task<IActionResult> AddPostAsync([FromBody] PostAddRequest request)
            => _viewMapper.ServiceResultToContentResult(
                await _postService.CreatePostAsync(request, UserId));
    }
}