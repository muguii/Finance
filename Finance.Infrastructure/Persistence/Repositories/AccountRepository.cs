﻿using Finance.Core.Entities;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infrastructure.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private const int PAGE_SIZE = 10;
        private readonly FinanceDbContext _dbContext;

        public AccountRepository(FinanceDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<PaginationResult<Account>> GetAllAsync(string? query, int page)
        {
            return await this.GetActive(query).GetPaged(page, PAGE_SIZE);
        }

        public async Task<List<Account>> GetAllWithDetailsAsync(string? query)
        {
            return await this.GetActive(query)
                             .Include(a => a.Transactions)
                             .ToListAsync();
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await _dbContext.Account.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Account> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.Account.Include(a => a.Transactions)
                                           .SingleOrDefaultAsync(a => a.Id == id);

        }

        public async Task AddAsync(Account account)
        {
            await _dbContext.AddAsync(account);
        }

        public void Remove(Account account)
        {
            _dbContext.Remove(account);
        }

        private IQueryable<Account> GetActive(string? query)
        {
            IQueryable<Account> activeAccounts = _dbContext.Account.Where(a => a.Active);

            if (string.IsNullOrWhiteSpace(query))
                return activeAccounts;

            return activeAccounts.Where(a => a.Description.Contains(query));
        }
    }
}
