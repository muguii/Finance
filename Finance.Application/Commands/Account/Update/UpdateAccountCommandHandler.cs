using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.Account.Update
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.Account.GetByIdAsync(request.AccountId);

            //if (account == null)
            //Exception?

            // TODO: Balance change must create a transaction

            account.Update(request.Description, request.Color, request.Balance);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
