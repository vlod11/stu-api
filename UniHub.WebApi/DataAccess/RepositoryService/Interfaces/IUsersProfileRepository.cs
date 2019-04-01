using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public interface IUsersProfileRepository : IBaseRepository<UsersProfile> 
    {
        Task<bool> IsUserExistByUsernameAsync(string username);
        Task<UsersProfile> GetUserWithCredentialsAsync(string email, bool excludeDeleted);
    }
}