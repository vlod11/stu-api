using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService.Repositories
{
    public class PostActionRepository : BaseRepository<PostAction>, IPostActionRepository
    {
        public PostActionRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}