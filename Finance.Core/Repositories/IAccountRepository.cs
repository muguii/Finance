using Finance.Core.Entities;

namespace Finance.Core.Repositories
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<List<Account>> GetAllWithTransactionsAsync(string? query);
        Task<Account> GetByIdWithTransactionsAsync(int id);

    }
}
