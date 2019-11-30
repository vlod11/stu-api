using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.Data.Entities;
using UniHub.Data.Interfaces;

namespace UniHub.Data.Repositories
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