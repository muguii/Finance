using Finance.Application.Mappers.Account;
using Finance.Application.Mappers.Address;
using Finance.Application.Mappers.Category;
using Finance.Application.ViewModels.User;

namespace Finance.Application.Mappers.User
{
    public static class UserToUserDetailsViewModel
    {
        public static UserDetailsViewModel ToUserDetailsViewModel(this Core.Entities.User user)
        {
            return new UserDetailsViewModel(user.Id,
                                            user.Login,
                                            user.Email,
                                            $"{user.Name.Trim()} {user.LastName?.Trim()}".Trim(),
                                            user.Birthdate,
                                            user.Gender,
                                            user.Telephone,
                                            user.Active,
                                            user.CreatedAt,
                                            user.LastUpdate,
                                            user.Address.ToAddressDetailsViewModel(),
                                            user.Accounts.Select(a => a.ToAccountDetailsViewModel()).ToList(),
                                            user.Categories.Select(c => c.ToCategoryDetailsViewModel()).ToList());
        }
    }
}
