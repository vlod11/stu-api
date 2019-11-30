using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.Data.Entities;
using UniHub.Data.Interfaces;

namespace UniHub.Data.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<City>> GetCitiesByCountryAsync(int countryId)
        {
            return await _dbContext.Countries.Where(c => c.Id == countryId)
                                                .SelectMany(x => x.Cities)
                                                .ToListAsync();
        }

        public async Task<bool> IsCityExistAsync(string cityTitle, int countryId)
        {
            return await _dbContext.Cities.Where(x => x.CountryId == countryId && x.Title == cityTitle)
                                            .AnyAsync();
        }
    }
}