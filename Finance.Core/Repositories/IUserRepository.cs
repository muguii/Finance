using Finance.Core.Entities;

namespace Finance.Core.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByIdWithAccountsAsync(int id);
    }
}
