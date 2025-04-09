using Domain;
using Shared;
using System.Linq.Expressions;

namespace Contracts.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> GetSingleAsync(
            Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<PaginatedData<TEntity>> GetPaginatedDataAsync(
            int pageIndex,
            int pageSize,
            string? filter = null,
            string? orderBy = null,
            CancellationToken cancellationToken = default);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(object id);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> ExistsAsync(object id);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
