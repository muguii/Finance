using Finance.Application.ViewModels;
using Finance.Core.Entities;

namespace Finance.Application.Mappers
{
    public static class TransactionToTransactionDetailsViewModel
    {
        public static TransactionDetailsViewModel ToTransactionDetailsViewModel(this Transaction transaction)
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
