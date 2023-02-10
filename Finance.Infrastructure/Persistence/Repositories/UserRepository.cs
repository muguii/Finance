using Finance.Core.Entities;
using Finance.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FinanceDbContext _dbContext;

        public UserRepository(FinanceDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<User>> GetAllAsync(string? query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return await _dbContext.User.ToListAsync();

            return await _dbContext.User.Where(u => u.Name.Contains(query) || 
                                              (!string.IsNullOrWhiteSpace(u.LastName) && u.LastName.Contains(query)) || 
                                               u.Login.Contains(query))
                                        .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.User.Include(u => u.Accounts)
                                        .SingleOrDefaultAsync(u => u.Id == id && u.Active);
        }

        public async Task<User> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.User.Include(u => u.Accounts)
                                        .Include(u => u.Accounts)
                                        .SingleOrDefaultAsync(u => u.Id == id && u.Active);
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.User.AddAsync(user);
        }
    }
}
