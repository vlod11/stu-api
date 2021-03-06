using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.Models.Enums;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Models;
using UniHub.WebApi.Models.Requests;

namespace UniHub.WebApi.BusinessLogic.Services.Contract
{
    public interface IPostService
    {
        Task<ServiceResult<IEnumerable<PostShortDto>>> GetPostsAsync(int facultyId, int userId,
                string title = "", int groupId = 0, int? semester = 0, EPostValueType? valueType = null, EPostLocationType? locationType = null, 
                DateTimeOffset? givenDateFrom = null, DateTimeOffset? givenDateTo = null, int skip = 0, int take = 0);
        Task<ServiceResult<IEnumerable<PostBySemesterGroupDto>>> GetListOfInitialPostsAsync(int facultyId, int userId,
            string title = "", int groupId = 0, int? semester = 0, EPostValueType? valueType = null, EPostLocationType? locationType = null,
            DateTimeOffset? givenDateFrom = null, DateTimeOffset? givenDateTo = null);
        Task<ServiceResult<IEnumerable<PostProfileDto>>> GetUsersPostsAsync(int userId, int skip, int take);
        Task<ServiceResult<PostLongDto>> GetPostFullInfoAsync(int postId, int userId, ERoleType userRole);
        Task<ServiceResult<PostLongDto>> CreatePostAsync(PostAddRequest request, int userId);
        Task<ServiceResult<PostLongDto>> VoteOnPostAsync(int postId, EPostVoteType postAction, int userId, ERoleType userRole);
    }
}