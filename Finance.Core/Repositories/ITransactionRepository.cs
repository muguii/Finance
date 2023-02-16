using Finance.Core.Entities;

namespace Finance.Core.Repositories
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        void RemoveRange(List<Transaction> transactions);
        void Remove(Transaction transaction);
    }
}
