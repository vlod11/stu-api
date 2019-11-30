using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.Data.Entities;
using UniHub.Data.Interfaces;

namespace UniHub.Data.Repositories
{
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByFacultyWithTeachersAsync(int facultyId, int skip, int take)
        {
            return await _dbContext.Subjects
                                    .Include(s => s.Teacher)
                                    .Where(s => s.FacultyId == facultyId)
                                    .OrderBy(s => s.Id)
                                    .Skip(skip).Take(take)
                                    .ToListAsync();
        }
    }
}