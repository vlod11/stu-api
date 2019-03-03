using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using UniHub.WebApi.ModelLayer.Entities;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        protected UniHubDbContext dbContext { get; set; }

        public RepositoryBase(UniHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> IsExistById(int id)
        {
            return await dbContext.Set<T>().Where(x => x.Id == id).AnyAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await dbContext.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, int skip = 0, int take = 0)
        {
            var result = dbContext.Set<T>().Where(expression);
            if (skip != 0)
            {
                result = result.Skip(skip);
            }

            if (take != 0)
            {
                result = result.Take(take);
            }

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public void Create(T entity)
        {
            dbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }
    }
}