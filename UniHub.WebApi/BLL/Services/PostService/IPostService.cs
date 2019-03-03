using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Requests;

namespace UniHub.WebApi.BLL.Services
{
    public interface IPostService
    {
        Task<ServiceResult<IEnumerable<PostCardDto>>> GetListOfPostCardsAsync(int facultyId, int skip, int take);
        Task<ServiceResult<PostLongDto>> GetPostFullInfoAsync(int postId);
        Task<ServiceResult<PostLongDto>> CreatePostAsync(PostAddRequest request, int userId);
    }
}