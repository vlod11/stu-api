using System.Threading.Tasks;
using System.Linq;
using UniHub.WebApi.ModelLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UniHub.WebApi.ModelLayer.ModelDto;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class UniversityRepository : BaseRepository<University>, IUniversityRepository
    {
        public UniversityRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<University>> GetAllUniversitiesAsync(int skip, int take)
        {
            return await _dbContext.Universities
                                        .OrderBy(u => u.Id)
                                        .Skip(skip).Take(take)
                                        .ToListAsync();
        }

        public async Task<IEnumerable<University>> GetUniversitiesByCityAsync(int cityId, int skip, int take)
        {
            return await _dbContext.Universities
                                        .Where(u => u.CityId == cityId)
                                        .OrderBy(u => u.Id)
                                        .Skip(skip).Take(take)
                                        .ToListAsync();
        }
    }
}