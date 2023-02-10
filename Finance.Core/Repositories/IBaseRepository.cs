namespace Finance.Core.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAllAsync(string? query);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
    }
}
