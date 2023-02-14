using Finance.Core.Entities;
using Finance.Infrastructure.Persistence;
using Finance.Infrastructure.Services.Auth;
using MediatR;

namespace Finance.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            this._unitOfWork = unitOfWork;
            this._authService = authService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            var user = new User(request.Login, passwordHash, request.Email, request.Name, request.LastName, request.Birthdate, request.Gender, request.Telephone, request.Role);

            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.User.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            var address = new Address(user.Id, request.Street, request.Number, request.PostalCode, request.District, request.City, request.State, request.Country);
            await _unitOfWork.User.AddAddressAsync(address);
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return user.Id;
        }
    }
}
