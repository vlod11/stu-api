using UniHub.Data.Entities;
using UniHub.Data.Interfaces;

namespace UniHub.Data.Repositories
{
    public class UserAvailablePostRepository : BaseRepository<UserAvailablePost>, IUserAvailablePostRepository
    {
        public UserAvailablePostRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}