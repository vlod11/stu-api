using System.Threading.Tasks;
using UniHub.Data.Entities;

namespace UniHub.Data.Interfaces
{
    public interface IUserRepository : IBaseRepository<User> 
    {
        Task<bool> IsUserExistByUsernameAsync(string username);
        Task<User> GetUserAsync(string email, bool excludeDeleted);
    }
}