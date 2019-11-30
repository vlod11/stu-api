using System.Threading.Tasks;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;

namespace UniHub.Services.Contract
{
    public interface IPostTradeService
    {
        Task<ServiceResult<PostLongDto>> UnlockPostAsync(int postId, int userId);
    }
}