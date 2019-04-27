using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.ModelDto;
using UniHub.WebApi.ModelLayer.Models;

namespace UniHub.WebApi.BLL.Services.Contract
{
    public interface ICountryService
    { 
        Task<ServiceResult<IEnumerable<CountryDto>>> GetListOfCountriesAsync();
        Task<ServiceResult<CountryDto>> CreateCountryAsync(string countryTitle);
    }
}