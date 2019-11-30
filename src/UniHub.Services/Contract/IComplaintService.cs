using System.Threading.Tasks;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;
using UniHub.Model.Request;

namespace UniHub.Services.Contract
{
    public interface IComplaintService
    {
        Task<ServiceResult<ComplaintDto>> CreateComplaintAsync(ComplaintAddRequest request, int userId);
    }
}