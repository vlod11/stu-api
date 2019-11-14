using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Models;
using UniHub.WebApi.Models.Requests;

namespace UniHub.WebApi.BusinessLogic.Services.Contract
{
    public interface ICityService
    { 
        Task<ServiceResult<IEnumerable<CityDto>>> GetListOfCitiesAsync(int countryId);
        Task<ServiceResult<CityDto>> CreateCityAsync(CityAddRequest request);
    }
}