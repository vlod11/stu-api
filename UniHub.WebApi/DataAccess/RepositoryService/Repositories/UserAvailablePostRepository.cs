using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.Models.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService.Repositories
{
    public class UserAvailablePostRepository : BaseRepository<UserAvailablePost>, IUserAvailablePostRepository
    {
        public UserAvailablePostRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}