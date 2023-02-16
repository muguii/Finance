using Finance.Application.ViewModels;

namespace Finance.Application.Mappers.Account
{
    public static class AccountToAccountViewModel
    {
        public static AccountViewModel ToAccountViewModel(this Core.Entities.Account account)
        {
            return new AccountViewModel(account.Description, account.Color, account.Balance);
        }
    }
}
