using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class UsersProfileRepository : RepositoryBase<UsersProfile>, IUsersProfileRepository
    {
        public UsersProfileRepository(UniHubDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<bool> IsUserExistByUsername(string username)
        {
            return await _dbContext.UserProfiles.Where(x => x.Username == username).AnyAsync();
        }

        public async Task<UsersProfile> GetUserWithCredentials(string email, bool excludeDeleted)
        {
            var users = _dbContext.UserProfiles.Include(up => up.Credentional)
                                                .Where(c => c.Credentional.Email == email);
            if (excludeDeleted)
            {
                users = users.Where(x => x.Credentional.DeletedAt == null);
            }
            
            return await users.FirstOrDefaultAsync();
        }
    }
}