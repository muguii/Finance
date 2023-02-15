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

        public async Task<List<Category>> GetAllWithDetailsAsync(string? query)
        {
            return await this.GetActive(query)
                             .Include(c => c.Transactions)
                             .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _dbContext.Category.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.Category.Include(c => c.Transactions)
                                            .SingleOrDefaultAsync(c => c.Id == id);

        }

        public async Task AddAsync(Category category)
        {
            await _dbContext.Category.AddAsync(category);
        }

        public void Remove(Category category)
        {
            _dbContext.Category.Remove(category);
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
