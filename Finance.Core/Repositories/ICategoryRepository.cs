using Finance.Core.Entities;

namespace Finance.Core.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<List<Category>> GetAllWithDetailsAsync(string? query);
        Task<Category> GetByIdWithDetailsAsync(int id);
        void Remove(Category category);
    }
}
