using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.Data.Entities;
using UniHub.Data.Interfaces;

namespace UniHub.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(UniHubDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<bool> IsUserExistByUsernameAsync(string username)
        {
            return await _dbContext.Users.Where(x => x.Username.ToLower() == username.ToLower()).AnyAsync();
        }

        public async Task<User> GetUserAsync(string email, bool excludeDeleted)
        {
            var users = _dbContext.Set<User>().Where(up => up.Email.ToLower() == email.ToLower());
            if (excludeDeleted)
            {
                users = users.Where(x => x.DeletedAtUtc == null);
            }

            return await users.FirstOrDefaultAsync();
        }
    }
}