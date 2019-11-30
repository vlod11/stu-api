using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.Model.Models;
using UniHub.Model.Read.ModelDto;

namespace UniHub.Services.Contract
{
    public interface ICountryService
    { 
        Task<ServiceResult<IEnumerable<CountryDto>>> GetListOfCountriesAsync();
        Task<ServiceResult<CountryDto>> CreateCountryAsync(string countryTitle);
    }
}