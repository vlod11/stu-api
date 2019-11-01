using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService.Repositories
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