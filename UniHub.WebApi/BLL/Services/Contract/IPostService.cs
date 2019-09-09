using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Enums;
using System;

namespace UniHub.WebApi.BLL.Services.Contract
{
    public interface IPostService
    {
        Task<ServiceResult<IEnumerable<PostBySemesterGroupDto>>> GetListOfPostsBySemesterGroupAsync(int facultyId, int userId, int skip, int take,
                string title = "", int groupId = 0, int? semester = 0, EPostValueType? valueType = null, EPostLocationType? locationType = null);
        Task<ServiceResult<IEnumerable<PostBySemesterGroupDto>>> GetListOfInitialPostsAsync(int facultyId, int userId,
            string title = "", int groupId = 0, int? semester = 0, EPostValueType? valueType = null, EPostLocationType? locationType = null,
            DateTimeOffset? createdFrom = null, DateTimeOffset? createdTo = null);
        Task<ServiceResult<IEnumerable<PostProfileDto>>> GetUsersPostsAsync(int userId, int skip, int take);
        Task<ServiceResult<PostLongDto>> GetPostFullInfoAsync(int postId, int userId, ERoleType userRole);
        Task<ServiceResult<PostLongDto>> CreatePostAsync(PostAddRequest request, int userId);
        Task<ServiceResult<PostLongDto>> VoteOnPostAsync(int postId, EPostVoteType postAction, int userId, ERoleType userRole);
    }
}