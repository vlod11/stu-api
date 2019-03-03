using UniHub.WebApi.Model;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public class CredentionalRepository : RepositoryBase<Credentional>, ICredentionalRepository
    {
        public CredentionalRepository(UniHubDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<bool> IsUserExistByEmail(string email)
        {
            return await dbContext.Credentials.Where(x => x.Email == email).FirstOrDefaultAsync() != null;
        }
    }
}