using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;
using UniHub.Model.Request;

namespace UniHub.Services.Contract
{
    public interface IFacultyService
    {
        Task<ServiceResult<IEnumerable<FacultyDto>>> GetListOfFacultiesAsync(int universityId, int skip, int take);
        Task<ServiceResult<FacultyDto>> CreateFacultyAsync(FacultyAddRequest request);
    }
}