using Finance.Core.Entities;

namespace Finance.Core.Repositories
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<List<Account>> GetAllWithDetailsAsync(string? query);
        Task<Account> GetByIdWithDetailsAsync(int id);

    }
}
