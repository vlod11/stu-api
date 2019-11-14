using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Models;
using UniHub.WebApi.Models.Requests;

namespace UniHub.WebApi.BusinessLogic.Services.Contract
{
    public interface IFacultyService
    {
        Task<ServiceResult<IEnumerable<FacultyDto>>> GetListOfFacultiesAsync(int universityId, int skip, int take);
        Task<ServiceResult<FacultyDto>> CreateFacultyAsync(FacultyAddRequest request);
    }
}