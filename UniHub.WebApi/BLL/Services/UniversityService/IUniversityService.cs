using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;

namespace UniHub.WebApi.BLL.Services
{
    public interface IUniversityService
    { 
        Task<ServiceResult<IEnumerable<UniversityDto>>> GetListOfUniversitiesAsync(int cityId, int skip, int take);
        Task<ServiceResult<UniversityDto>> CreateUniversityAsync(UniversityAddRequest request);
    }
}