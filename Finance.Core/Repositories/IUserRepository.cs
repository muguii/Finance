using Finance.Core.Entities;

namespace Finance.Core.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByIdWithDetailsAsync(int id);
        Task<User> GetByLoginAndPasswordAsync(string login, string passwordHash);
        Task AddAddressAsync(Address address);
    }
}
