using Finance.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Finance.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FinanceDbContext _context;
        private IDbContextTransaction _transaction;

        public IAccountRepository Account { get; }
        public ICategoryRepository Category { get; }
        public ITransactionRepository Transaction { get; }
        public IUserRepository User { get; }

        public UnitOfWork(FinanceDbContext context, 
                          IAccountRepository account, 
                          ICategoryRepository category,
                          ITransactionRepository transaction, 
                          IUserRepository user)
        {
            _context = context;

            Account = account;
            Category = category;
            Transaction = transaction;
            User = user;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
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
                _context.Dispose();
        }
    }
}
