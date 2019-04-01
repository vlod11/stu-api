using System.Collections.Generic;
using System.Threading.Tasks;
using UniHub.WebApi.Model;
using System.Linq;
using UniHub.WebApi.ModelLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class FacultyRepository : BaseRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(UniHubDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Faculty>> GetFacultiesByUniversityAsync(int universityId, int skip, int take)
        {
            return await _dbContext.Faculties
                                    .Where(f => f.UniversityId == universityId)
                                    .Skip(skip).Take(take)
                                    .OrderBy(f => f.FullTitle)
                                    .ToListAsync();
        }
    }
}