namespace Finance.Application.ViewModels.User
{
    public class LoginUserViewModel
    {
        public string Login { get; private set; }
        public string Email { get; private set; }
        public string Token { get; private set; }

        public LoginUserViewModel(string login, string email, string token)
        {
            Login = login;
            Email = email;
            Token = token;
        }
    }
}
