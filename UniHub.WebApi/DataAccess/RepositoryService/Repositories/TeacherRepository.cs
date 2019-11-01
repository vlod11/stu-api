using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}