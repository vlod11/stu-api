using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.Data.Entities;

namespace UniHub.Data.Interfaces
{
    public interface ICityRepository : IBaseRepository<City>
    {
        Task<IEnumerable<City>> GetCitiesByCountryAsync(int countryId);
        Task<bool> IsCityExistAsync(string cityTitle, int countryId);
    }
}