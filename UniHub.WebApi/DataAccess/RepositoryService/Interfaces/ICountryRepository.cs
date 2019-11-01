using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService.Interfaces
{
    public interface ICountryRepository : IBaseRepository<Country>
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
    }
}