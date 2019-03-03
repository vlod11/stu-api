using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public interface IUsersProfileRepository : IRepositoryBase<UsersProfile> 
    {
        Task<bool> IsUserExistByUsername(string username);
        Task<UsersProfile> GetUserWithCredentials(string email, bool excludeDeleted);
    }
}