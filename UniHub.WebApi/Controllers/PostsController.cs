using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BLL.Services;
using UniHub.WebApi.BLL.Services.Contract;
using UniHub.WebApi.Helpers.Mappers;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Enums;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Requests;

namespace UniHub.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{api-version:apiVersion}/[controller]")]
    [ApiController]
    public class PostsController : BaseController
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
        public async Task<ActionResult<IEnumerable<PostCardDto>>> GetPostCardsAsync(int subjectId, int skip = 0, int take = 10)
        => _viewMapper.ServiceResultToContentResult(
                await _postService.GetListOfPostCardsAsync(subjectId, skip, take));

        [HttpGet("{id}")]
        public async Task<ActionResult<PostLongDto>> GetPostFullInfoAsync([FromRoute] int id)
        => _viewMapper.ServiceResultToContentResult(
                await _postService.GetPostFullInfoAsync(id));

        [HttpPost]
        [Authorize(Roles = nameof(ERoleType.Admin) + ", " + nameof(ERoleType.Student))]
        public async Task<ActionResult<PostLongDto>> AddPostAsync([FromBody] PostAddRequest request)
            => _viewMapper.ServiceResultToContentResult(
                await _postService.CreatePostAsync(request, UserId));

        [HttpPost("{postId}/act")]
        [Authorize(Roles = nameof(ERoleType.Admin) + ", " + nameof(ERoleType.Student))]
        public async Task<IActionResult> SetAction([FromRoute] int postId, EPostActionType postAction)
            => _viewMapper.ServiceResultToContentResult(
                await _postService.ActionOnPostAsync(postId, postAction, UserId));
    }
}