using Finance.Core.Entities;

namespace Finance.Core.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<List<Category>> GetAllWithTransactionsAsync(string? query);
        Task<Category> GetByIdWithTransactionsAsync(int id);

    }
}
