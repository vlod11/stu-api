using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;

namespace UniHub.WebApi.BLL.Services.Contract
{
    public interface IPostTradeService
    {
        Task<ServiceResult<PostLongDto>> UnlockPostAsync(int postId, int userId);
    }
}