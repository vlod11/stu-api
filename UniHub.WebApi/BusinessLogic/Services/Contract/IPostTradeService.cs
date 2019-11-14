using System.Threading.Tasks;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Models;

namespace UniHub.WebApi.BusinessLogic.Services.Contract
{
    public interface IPostTradeService
    {
        Task<ServiceResult<PostLongDto>> UnlockPostAsync(int postId, int userId);
    }
}