using Finance.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Finance.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FinanceDbContext _dbContext;
        private IDbContextTransaction _transaction;

        public IAccountRepository Account { get; }
        public ICategoryRepository Category { get; }
        public ITransactionRepository Transaction { get; }
        public IUserRepository User { get; }

        public UnitOfWork(FinanceDbContext dbContext, 
                          IAccountRepository account, 
                          ICategoryRepository category,
                          ITransactionRepository transaction, 
                          IUserRepository user)
        {
            this._dbContext = dbContext;

            this.Account = account;
            this.Category = category;
            this.Transaction = transaction;
            this.User = user;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception)
            {
                await _transaction.RollbackAsync();
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
                _dbContext.Dispose();
        }
    }
}
