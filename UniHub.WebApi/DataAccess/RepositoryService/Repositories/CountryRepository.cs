using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await _dbContext.Countries.ToListAsync();
        }
    }
}