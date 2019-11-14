using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.Models.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService.Repositories
{
    public class FacultyRepository : BaseRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Faculty>> GetFacultiesByUniversityAsync(int universityId, int skip, int take)
        {
            return await _dbContext.Faculties
                                    .Where(f => f.UniversityId == universityId)
                                    .OrderBy(f => f.Id)
                                    .Skip(skip).Take(take)
                                    .ToListAsync();
        }
    }
}