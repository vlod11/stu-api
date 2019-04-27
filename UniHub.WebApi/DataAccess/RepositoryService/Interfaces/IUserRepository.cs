using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public interface IUserRepository : IBaseRepository<User> 
    {
        Task<bool> IsUserExistByUsernameAsync(string username);
        Task<User> GetUserAsync(string email, bool excludeDeleted);
    }
}