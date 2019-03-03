using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public interface IRepositoryBase<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, int skip, int take);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> IsExistById(int id);
    }
}