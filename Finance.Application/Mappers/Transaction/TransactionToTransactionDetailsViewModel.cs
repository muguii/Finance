using Finance.Application.Mappers.Account;
using Finance.Application.Mappers.Category;
using Finance.Application.ViewModels.Transaction;

namespace Finance.Application.Mappers.Transaction
{
    public static class TransactionToTransactionDetailsViewModel
    {
        public static TransactionDetailsViewModel ToTransactionDetailsViewModel(this Core.Entities.Transaction transaction)
        {
            return new TransactionDetailsViewModel(transaction.Id,
                                                   transaction.Description,
                                                   transaction.Date,
                                                   transaction.Value,
                                                   transaction.CreatedAt,
                                                   transaction.LastUpdate,
                                                   transaction.Account.ToAccountViewModel(),
                                                   transaction.Category.ToCategoryViewModel());
        }
    }
}
