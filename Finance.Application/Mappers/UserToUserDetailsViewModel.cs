using Finance.Application.ViewModels;
using Finance.Core.Entities;

namespace Finance.Application.Mappers
{
    public static class UserToUserDetailsViewModel
    {
        public static UserDetailsViewModel ToUserDetailsViewModel(this User user)
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
