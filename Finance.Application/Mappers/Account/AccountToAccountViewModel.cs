using Finance.Application.ViewModels.Account;

namespace Finance.Application.Mappers.Account
{
    public static class AccountToAccountViewModel
    {
        public static AccountViewModel ToAccountViewModel(this Core.Entities.Account account)
        {
            return new AccountViewModel(account.Id, account.Description, account.Color, account.Balance);
        }
    }
}
