using Finance.Core.Entities;

namespace Finance.Core.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task AddRangeAsync(List<Category> categories);
        Task<List<Category>> GetAllWithDetailsAsync(string? query);
        Task<Category> GetByIdWithDetailsAsync(int id);
        void Remove(Category category);
    }
}
