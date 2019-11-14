using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.Models.ModelDto;
using UniHub.WebApi.Models.Models;

namespace UniHub.WebApi.BusinessLogic.Services.Contract
{
    public interface ICountryService
    { 
        Task<ServiceResult<IEnumerable<CountryDto>>> GetListOfCountriesAsync();
        Task<ServiceResult<CountryDto>> CreateCountryAsync(string countryTitle);
    }
}