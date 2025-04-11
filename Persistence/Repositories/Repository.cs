using Contracts.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Shared;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly AppDbContext _dbContext;
        protected readonly DbSet<T> dbSet;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public virtual Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        }

        public virtual Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().FirstOrDefaultAsync(predicate)!;
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return (await _dbContext.Set<T>().FindAsync(id))!;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null!,
            CancellationToken cancellationToken = default)
        {
            return await (predicate == null ?
                _dbContext.Set<T>().ToListAsync(cancellationToken) :
                _dbContext.Set<T>().Where(predicate).ToListAsync(cancellationToken));
        }

        public virtual void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public virtual async Task Delete(object id)
        {
            T entity = (await _dbContext.Set<T>().FindAsync(id))!;
            if (entity == null)
                return;
            _dbContext.Set<T>().Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            dbSet.UpdateRange(entities);
        }

        public Task<PaginatedData<T>> GetPaginatedDataAsync(int pageIndex, int pageSize, string? filter = null, string? orderBy = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
