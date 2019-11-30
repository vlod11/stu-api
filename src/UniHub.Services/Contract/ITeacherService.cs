using System.Threading.Tasks;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;
using UniHub.Model.Request;

namespace UniHub.Services.Contract
{
    public interface ITeacherService
    {
        Task<ServiceResult<TeacherDto>> CreateTeacherAsync(TeacherAddRequest request);
    }
}