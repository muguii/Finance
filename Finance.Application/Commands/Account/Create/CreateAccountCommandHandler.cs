using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.Account.Create
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Core.Entities.Account(request.Description, request.Color, request.InitialBalance, request.InitialBalance, request.UserId);

            await _unitOfWork.Account.AddAsync(account);
            await _unitOfWork.CompleteAsync();

            return account.Id;
        }
    }
}
