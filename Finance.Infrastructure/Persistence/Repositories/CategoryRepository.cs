using Finance.Core.Entities;
using Finance.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FinanceDbContext _dbContext;

        public CategoryRepository(FinanceDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Category>> GetAllAsync(string? query)
        {
            return await this.GetActive(query).ToListAsync();
        }

        public async Task<List<Category>> GetAllWithTransactionsAsync(string? query)
        {
            return await this.GetActive(query)
                             .Include(c => c.Transactions)
                             .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _dbContext.Category.SingleOrDefaultAsync(c => c.Id == id && c.Active);
        }

        public async Task<Category> GetByIdWithTransactionsAsync(int id)
        {
            return await _dbContext.Category.Include(c => c.Transactions)
                                            .SingleOrDefaultAsync(c => c.Id == id && c.Active);

        }

        public async Task AddAsync(Category category)
        {
            await _dbContext.Category.AddAsync(category);
        }

        private IQueryable<Category> GetActive(string? query)
        {
            IQueryable<Category> activeCategories = _dbContext.Category.Where(c => c.Active);

            if (string.IsNullOrWhiteSpace(query))
                return activeCategories;

            return activeCategories.Where(c => c.Description.Contains(query)); ;
        }
    }
}
