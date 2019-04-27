using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(UniHubDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<bool> IsUserExistByUsernameAsync(string username)
        {
            return await _dbContext.Users.Where(x => x.Username.ToUpperInvariant() == username.ToUpperInvariant()).AnyAsync();
        }

        public async Task<User> GetUserAsync(string email, bool excludeDeleted)
        {
            var users = _dbContext.Users.Where(up => up.Email.ToUpperInvariant() == email.ToUpperInvariant());
            if (excludeDeleted)
            {
                users = users.Where(x => x.DeletedAt == null);
            }
            
            return await users.FirstOrDefaultAsync();
        }
    }
}