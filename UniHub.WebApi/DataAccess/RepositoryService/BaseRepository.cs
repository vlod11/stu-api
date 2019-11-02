using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using UniHub.WebApi.ModelLayer.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace UniHub.WebApi.DataAccess.RepositoryService
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected UniHubDbContext _dbContext { get; set; }

        public BaseRepository(UniHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking()
                                    .Where(predicate)
                                    .AnyAsync();
        }

        public async Task<bool> IsExistById(int id)
        {
            return await _dbContext.Set<T>().Where(x => x.Id == id).AnyAsync();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetSingleAsync(params Expression<Func<T, bool>>[] predicates)
        {
            IQueryable<T> entities = _dbContext.Set<T>();

            foreach (var predicate in predicates)
            {
                entities = entities.Where(predicate);
            }

            return await entities.FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindByAsync(int skip, int take, params Expression<Func<T, bool>>[] predicates)
        {
            IQueryable<T> entities = _dbContext.Set<T>();

            foreach (var predicate in predicates)
            {
                entities = entities.Where(predicate);
            }

            if (skip != 0)
            {
                entities = entities.Skip(skip);
            }

            if (take != 0)
            {
                entities = entities.Take(take);
            }

            return await entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public virtual async Task AddAsync(T entity)
        {
            EntityEntry dbEntityEntry = _dbContext.Entry<T>(entity);
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}