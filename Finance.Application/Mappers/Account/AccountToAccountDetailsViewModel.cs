using Finance.Application.Mappers.Transaction;
using Finance.Application.ViewModels;

namespace Finance.Application.Mappers.Account
{
    public static class AccountToAccountDetailsViewModel
    {
        public static AccountDetailsViewModel ToAccountDetailsViewModel(this Core.Entities.Account account)
        {
            return new AccountDetailsViewModel(account.Description,
                                               account.Color,
                                               account.Balance,
                                               account.InitialBalance,
                                               account.Active,
                                               account.CreatedAt,
                                               account.LastUpdate,
                                               account.Transactions.Select(t => t.ToTransactionDetailsViewModel()).ToList());
        }
    }
}
