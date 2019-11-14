using System.Threading.Tasks;
using UniHub.WebApi.Models.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService.Interfaces
{
    public interface IUserRepository : IBaseRepository<User> 
    {
        Task<bool> IsUserExistByUsernameAsync(string username);
        Task<User> GetUserAsync(string email, bool excludeDeleted);
    }
}