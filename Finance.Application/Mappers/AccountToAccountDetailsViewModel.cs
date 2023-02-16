using Finance.Application.ViewModels;
using Finance.Core.Entities;

namespace Finance.Application.Mappers
{
    public static class AccountToAccountDetailsViewModel
    {
        public static AccountDetailsViewModel ToAccountDetailsViewModel(this Account account)
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
