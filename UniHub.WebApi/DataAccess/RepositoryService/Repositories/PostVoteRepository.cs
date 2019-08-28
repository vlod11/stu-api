using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService.Repositories
{
    public class PostVoteRepository : BaseRepository<PostVote>, IPostVoteRepository
    {
        public PostVoteRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}