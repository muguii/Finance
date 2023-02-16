using Finance.Application.ViewModels.Transaction;

namespace Finance.Application.Mappers.Transaction
{
    public static class TransactionToTransactionViewModel
    {
        public static TransactionViewModel ToTransactionViewModel(this Core.Entities.Transaction transaction)
        {
            return new TransactionViewModel(transaction.Description, transaction.Date, transaction.Value, transaction.CreatedAt, transaction.LastUpdate);
        }
    }
}
