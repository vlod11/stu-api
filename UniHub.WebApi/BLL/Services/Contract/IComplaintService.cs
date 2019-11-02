using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;

namespace UniHub.WebApi.BLL.Services.Contract
{
    public interface IComplaintService
    {
        Task<ServiceResult<ComplaintDto>> CreateComplaintAsync(ComplaintAddRequest request, int userId);
    }
}