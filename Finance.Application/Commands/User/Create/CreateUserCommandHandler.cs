using Finance.Core.Entities;
using Finance.Core.Enums;
using Finance.Infrastructure.Persistence;
using Finance.Infrastructure.Services.Auth;
using MediatR;

namespace Finance.Application.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            var user = new Core.Entities.User(request.Login, passwordHash, request.Email, request.Name, request.LastName, request.Birthdate, request.Gender, request.Telephone, request.Role);

            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.User.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            var address = new Address(user.Id, request.Street, request.Number, request.PostalCode, request.District, request.City, request.State, request.Country);
            await _unitOfWork.User.AddAddressAsync(address);

            var defaultAccount = new Core.Entities.Account("Wallet", "brown", 0M, user.Id);
            await _unitOfWork.Account.AddAsync(defaultAccount);

            var defaultCategories = new List<Core.Entities.Category>
            {
                new Core.Entities.Category("Salary", "green", CategoryType.Income, user.Id),
                new Core.Entities.Category("Food", "blue", CategoryType.Expense, user.Id),
                new Core.Entities.Category("Leisure", "purple", CategoryType.Expense, user.Id),
                new Core.Entities.Category("Financing", "yellow", CategoryType.Expense, user.Id)
            };
            await _unitOfWork.Category.AddRangeAsync(defaultCategories);

            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return user.Id;
        }
    }
}
