using Finance.Application.ViewModels.User;
using Finance.Infrastructure.Persistence;
using Finance.Infrastructure.Services.Auth;
using MediatR;

namespace Finance.Application.Commands.User.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);
            var user = await _unitOfWork.User.GetByLoginAndPasswordAsync(request.Login, passwordHash);

            if (user == null)
                return null;

            var jwtToken = _authService.GenerateJwtToken(user.Login, user.Email, user.Role);
            return new LoginUserViewModel(user.Login, user.Email, jwtToken);
        }
    }
}
