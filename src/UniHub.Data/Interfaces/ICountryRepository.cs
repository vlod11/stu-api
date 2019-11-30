using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.Data.Entities;

namespace UniHub.Data.Interfaces
{
    public interface ICountryRepository : IBaseRepository<Country>
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
    }
}