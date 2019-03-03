using System.Threading.Tasks;
using UniHub.WebApi.Model;
using System.Linq;
using UniHub.WebApi.ModelLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UniHub.WebApi.ModelLayer.ModelDto;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        public CityRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<City>> GetCitiesByCountryAsync(int countryId)
        {
            return await dbContext.Countries.Where(c => c.Id == countryId)
                                                .SelectMany(x => x.Cities)
                                                .ToListAsync();
        }

        public async Task<bool> IsCityExist(string cityTitle, int countryId)
        {
            return await dbContext.Cities.Where(x => x.CountryId == countryId && x.Title == cityTitle)
                                            .AnyAsync();
        }
    }
}