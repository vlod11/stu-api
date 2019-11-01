using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService.Interfaces
{
    public interface ICityRepository : IBaseRepository<City>
    {
        Task<IEnumerable<City>> GetCitiesByCountryAsync(int countryId);
        Task<bool> IsCityExistAsync(string cityTitle, int countryId);
    }
}