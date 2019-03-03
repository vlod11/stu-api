using System.Threading.Tasks;
using System.Linq;
using UniHub.WebApi.ModelLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UniHub.WebApi.ModelLayer.ModelDto;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class UniversityRepository : RepositoryBase<University>, IUniversityRepository
    {
        public UniversityRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<University>> GetAllUniversitiesAsync(int skip, int take)
        {
            return await dbContext.Universities
                        .Skip(skip).Take(take)
                        .OrderBy(u => u.Id)
                        .ToListAsync();
        }

        public async Task<IEnumerable<University>> GetUniversitiesByCityAsync(int cityId, int skip, int take)
        {
            return await dbContext.Universities
                                    .Where(u => u.CityId == cityId)
                                    .Skip(skip).Take(take)
                                    .OrderBy(u => u.Id)
                                    .ToListAsync();
        }
    }
}