using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public interface ICredentionalRepository : IRepositoryBase<Credentional> 
    {
        Task<bool> IsUserExistByEmail(string email);
    }
}