using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;
using UniHub.Model.Request;

namespace UniHub.Services.Contract
{
    public interface IUniversityService
    { 
        Task<ServiceResult<IEnumerable<UniversityDto>>> GetListOfUniversitiesAsync(int cityId, int skip, int take);
        Task<ServiceResult<UniversityDto>> CreateUniversityAsync(UniversityAddRequest request);
    }
}