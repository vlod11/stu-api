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
        private readonly IPostTradeService _postTradeService;

        public PostsController(
            IServiceResultMapper viewMapper,
            IPostService postService,
            IPostTradeService postTradeService)
        {
            _viewMapper = viewMapper;
            _postService = postService;
            _postTradeService = postTradeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostCardDto>>> GetPostCardsAsync(int subjectId, int skip = 0, int take = 10)
        => _viewMapper.ServiceResultToContentResult(
                await _postService.GetListOfPostCardsAsync(subjectId, UserId, skip, take));

        [HttpGet("{id}")]
        [Authorize(Roles = nameof(ERoleType.Admin) + ", " + nameof(ERoleType.Student))]
        public async Task<ActionResult<PostLongDto>> GetPostFullInfoAsync([FromRoute] int id)
        => _viewMapper.ServiceResultToContentResult(
                await _postService.GetPostFullInfoAsync(id, UserId, UserRole));

        [HttpPost]
        [Authorize(Roles = nameof(ERoleType.Admin) + ", " + nameof(ERoleType.Student))]
        public async Task<ActionResult<PostLongDto>> AddPostAsync([FromBody] PostAddRequest request)
            => _viewMapper.ServiceResultToContentResult(
                await _postService.CreatePostAsync(request, UserId));

        [HttpPost("{postId}/vote")]
        [Authorize(Roles = nameof(ERoleType.Admin) + ", " + nameof(ERoleType.Student))]
        public async Task<ActionResult<PostLongDto>> Vote([FromRoute] int postId, EPostVoteType postAction)
            => _viewMapper.ServiceResultToContentResult(
                await _postService.VoteOnPostAsync(postId, postAction, UserId));

        [HttpPost("{postId}/unlock")]
        [Authorize(Roles = nameof(ERoleType.Admin) + ", " + nameof(ERoleType.Student))]
        public async Task<ActionResult<PostLongDto>> Unlock([FromRoute] int postId)
            => _viewMapper.ServiceResultToContentResult(
                await _postTradeService.UnlockPostAsync(postId, UserId));
    }
}