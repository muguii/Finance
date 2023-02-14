using Finance.Application.ViewModels;
using Finance.Core.Entities;

namespace Finance.Application.Mappers
{
    public static class UserToUserViewModel
    {
        public static UserViewModel ToUserViewModel(this User user)
        {
            return new UserViewModel(user.Id, $"{user.Name.Trim()} {user.LastName?.Trim()}".Trim(), user.Birthdate, user.Gender);
        }
    }
}
