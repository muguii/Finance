using Finance.Application.ViewModels;

namespace Finance.Application.Mappers.User
{
    public static class UserToUserViewModel
    {
        public static UserViewModel ToUserViewModel(this Core.Entities.User user)
        {
            return new UserViewModel(user.Id, $"{user.Name.Trim()} {user.LastName?.Trim()}".Trim(), user.Birthdate, user.Gender);
        }
    }
}
