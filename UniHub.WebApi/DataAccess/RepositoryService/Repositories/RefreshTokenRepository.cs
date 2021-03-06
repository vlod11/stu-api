using UniHub.WebApi.DataAccess.RepositoryService.Interfaces;
using UniHub.WebApi.Models.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}