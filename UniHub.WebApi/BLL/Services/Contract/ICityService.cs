using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;
using UniHub.WebApi.ModelLayer.Requests;

namespace UniHub.WebApi.BLL.Services.Contract
{
    public interface ICityService
    { 
        Task<ServiceResult<IEnumerable<CityDto>>> GetListOfCitiesAsync(int countryId);
        Task<ServiceResult<CityDto>> CreateCityAsync(CityAddRequest request);
    }
}