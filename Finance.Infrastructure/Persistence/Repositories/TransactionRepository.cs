using Finance.Core.Entities;
using Finance.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionRepository(FinanceDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Transaction>> GetAllAsync(string? query)
        {
            IQueryable<Transaction> transactions = _dbContext.Transaction.Include(t => t.Account)
                                                                         .Include(t => t.Category);
            if (string.IsNullOrEmpty(query))
                return await transactions.ToListAsync();

            return await transactions.Where(t => t.Description.Contains(query)).ToListAsync();
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
