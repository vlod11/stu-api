using UniHub.Data.Entities;
using UniHub.Data.Interfaces;

namespace UniHub.Data.Repositories
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(UniHubDbContext dbContext) : base(dbContext)
        {
        }
    }
}