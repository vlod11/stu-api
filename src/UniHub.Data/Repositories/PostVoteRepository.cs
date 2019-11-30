using UniHub.Data.Entities;
using UniHub.Data.Interfaces;

namespace UniHub.Data.Repositories
{
    public class PostVoteRepository : BaseRepository<PostVote>, IPostVoteRepository
    {
        public PostVoteRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}