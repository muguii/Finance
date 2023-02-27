using Finance.Application.ViewModels.User;
using MediatR;

namespace Finance.Application.Commands.User.Login
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
