using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHub.WebApi.BusinessLogic.Services.Contract;
using UniHub.WebApi.Models.Enums;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Requests;
using UniHub.WebApi.Web.Helpers.Mappers;

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
        [Authorize]
        public async Task<ActionResult<IEnumerable<PostShortDto>>> GetPostsAsync(int subjectId, 
                string title = "", int groupId = 0, int? semester = 0, EPostValueType? valueType = null, EPostLocationType? locationType = null,
                 DateTimeOffset? givenDateFrom = null, DateTimeOffset? givenDateTo = null, int skip = 0, int take = 0)
        => _viewMapper.ServiceResultToContentResult(
                await _postService.GetPostsAsync(subjectId, UserId, title, groupId, semester, valueType, locationType, givenDateFrom, givenDateTo, skip, take));

        [HttpGet("initial")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PostBySemesterGroupDto>>> GetInitialPostsAsync(int subjectId, 
                string title = "", int groupId = 0, int? semester = 0, EPostValueType? valueType = null, EPostLocationType? locationType = null,
                 DateTimeOffset? givenDateFrom = null, DateTimeOffset? givenDateTo = null)
        => _viewMapper.ServiceResultToContentResult(
                await _postService.GetListOfInitialPostsAsync(subjectId, UserId, title, groupId, semester, valueType, locationType, givenDateFrom, givenDateTo));

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PostLongDto>> GetPostFullInfoAsync([FromRoute] int id)
        => _viewMapper.ServiceResultToContentResult(
                await _postService.GetPostFullInfoAsync(id, UserId, UserRole));

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostLongDto>> AddPostAsync([FromBody] PostAddRequest request)
            => _viewMapper.ServiceResultToContentResult(
                await _postService.CreatePostAsync(request, UserId));

        [HttpPost("{postId}/vote")]
        [Authorize]
        public async Task<ActionResult<PostLongDto>> Vote([FromRoute] int postId, EPostVoteType postAction)
            => _viewMapper.ServiceResultToContentResult(
                await _postService.VoteOnPostAsync(postId, postAction, UserId, UserRole));

        [HttpPost("{postId}/unlock")]
        [Authorize(Roles = nameof(ERoleType.Admin) + ", " + nameof(ERoleType.Student))]
        public async Task<ActionResult<PostLongDto>> Unlock([FromRoute] int postId)
            => _viewMapper.ServiceResultToContentResult(
                await _postTradeService.UnlockPostAsync(postId, UserId));
    }
}