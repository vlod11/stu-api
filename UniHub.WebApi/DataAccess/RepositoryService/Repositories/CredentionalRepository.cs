using UniHub.WebApi.Model;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class CredentionalRepository : BaseRepository<Credentional>, ICredentionalRepository
    {
        public CredentionalRepository(UniHubDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<bool> IsUserExistByEmailAsync(string email)
        {
            return await _dbContext.Credentials.Where(x => x.Email == email).FirstOrDefaultAsync() != null;
        }
    }
}