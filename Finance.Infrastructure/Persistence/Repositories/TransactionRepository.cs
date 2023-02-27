using Finance.Core.Entities;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private const int PAGE_SIZE = 10;
        private readonly FinanceDbContext _dbContext;

        public TransactionRepository(FinanceDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<PaginationResult<Transaction>> GetAllAsync(string? query, int page)
        {
            IQueryable<Transaction> transactions = _dbContext.Transaction.Include(t => t.Account)
                                                                         .Include(t => t.Category);
            if (string.IsNullOrEmpty(query))
                return await transactions.GetPaged(page, PAGE_SIZE);

            return await transactions.Where(t => t.Description.Contains(query)).GetPaged(page, PAGE_SIZE);
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _dbContext.Transaction.Include(a => a.Account)
                                               .Include(a => a.Category)
                                               .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _dbContext.Transaction.AddAsync(transaction);
        }

        public void RemoveRange(List<Transaction> transactions)
        {
            if (transactions.Any())
                _dbContext.Transaction.RemoveRange(transactions);
        }

        public void Remove(Transaction transaction)
        {
            _dbContext.Transaction.Remove(transaction);
        }
    }
}
