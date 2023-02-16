using Finance.Application.Mappers.Category;
using Finance.Application.ViewModels;

namespace Finance.Application.Mappers.Transaction
{
    public static class TransactionToTransactionDetailsViewModel
    {
        public static TransactionDetailsViewModel ToTransactionDetailsViewModel(this Core.Entities.Transaction transaction)
        {
            return new TransactionDetailsViewModel(transaction.Description,
                                                   transaction.Date,
                                                   transaction.Value,
                                                   transaction.CreatedAt,
                                                   transaction.LastUpdate,
                                                   transaction.Category.ToCategoryViewModel());
        }
    }
}
