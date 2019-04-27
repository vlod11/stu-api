using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Requests;
using UniHub.WebApi.ModelLayer.Entities;
using UniHub.WebApi.ModelLayer.Enums;

namespace UniHub.WebApi.BLL.Services.Contract
{
    public interface IPostService
    {
        Task<ServiceResult<IEnumerable<PostCardDto>>> GetListOfPostCardsAsync(int facultyId, int skip, int take);
        Task<ServiceResult<IEnumerable<PostProfileDto>>> GetUsersPostsAsync(int userId, int skip, int take);
        Task<ServiceResult<PostLongDto>> GetPostFullInfoAsync(int postId);
        Task<ServiceResult<PostLongDto>> CreatePostAsync(PostAddRequest request, int userId);
        Task<ServiceResult<Post>> ActionOnPostAsync(int postId, EPostActionType postAction, int userId);
    }
}