using Finance.Core.Repositories;

namespace Finance.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        IAccountRepository Account { get; }
        ICategoryRepository Category { get; }
        ITransactionRepository Transaction { get; }
        IUserRepository User { get; }
        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
    }
}
