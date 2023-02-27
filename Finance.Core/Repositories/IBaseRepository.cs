using Finance.Core.Models;

namespace Finance.Core.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<PaginationResult<T>> GetAllAsync(string? query, int page = 1);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
    }
}
