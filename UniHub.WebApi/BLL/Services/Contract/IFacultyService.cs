using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;

namespace UniHub.WebApi.BLL.Services.Contract
{
    public interface IFacultyService
    {
        Task<ServiceResult<IEnumerable<FacultyDto>>> GetListOfFacultiesAsync(int universityId, int skip, int take);
        Task<ServiceResult<FacultyDto>> CreateFacultyAsync(FacultyAddRequest request);
    }
}