using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        public FileRepository(UniHubDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}