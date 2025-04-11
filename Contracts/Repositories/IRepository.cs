using Domain;
using Shared;
using System.Linq.Expressions;

namespace Contracts.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<PaginatedData<TEntity>> GetPaginatedDataAsync(
            int pageIndex,
            int pageSize,
            string? filter = null,
            string? orderBy = null,
            CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        Task Delete(object id);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
