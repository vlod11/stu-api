using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;
using UniHub.Model.Request;

namespace UniHub.Services.Contract
{
    public interface ICityService
    { 
        Task<ServiceResult<IEnumerable<CityDto>>> GetListOfCitiesAsync(int countryId);
        Task<ServiceResult<CityDto>> CreateCityAsync(CityAddRequest request);
    }
}