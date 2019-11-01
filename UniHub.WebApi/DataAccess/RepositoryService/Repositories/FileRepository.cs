using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService.Repositories
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        public FileRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}