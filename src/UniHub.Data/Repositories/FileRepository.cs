using UniHub.Data.Entities;
using UniHub.Data.Interfaces;

namespace UniHub.Data.Repositories
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        public FileRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}