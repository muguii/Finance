using Finance.Application.ViewModels;
using Finance.Core.Entities;

namespace Finance.Application.Mappers
{
    public static class AccountToAccountViewModel
    {
        public static AccountViewModel ToAccountViewModel(this Account account)
        {
            return new AccountViewModel(account.Description, account.Color, account.Balance);
        }
    }
}
