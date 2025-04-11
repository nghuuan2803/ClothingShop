using Shared.Utils;

namespace WebApp.Client.Data
{
    public interface IDataRepository<T>
    {
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<PaginatedData<T>> GetPaginatedDataAsync(int pageIndex, int pageSize, string? filter, string? OrderBy);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(object id);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteRangeAsync(IEnumerable<T> entities);
        Task<bool> DeleteRangeAsync(IEnumerable<object> ids);
        Task<bool> ExistsAsync(object id);
    }
}
