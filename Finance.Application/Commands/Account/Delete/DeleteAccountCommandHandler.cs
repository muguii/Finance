using Finance.Core.Exceptions;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.Account.Delete
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.Account.GetByIdWithDetailsAsync(request.Id);

            if (account == null)
                throw new AccountNotExistsException(request.Id);

            //if (account.Active)
            // TODO: Can only delete accounts that are disabled (Shelved)

            await _unitOfWork.BeginTransactionAsync();

            _unitOfWork.Transaction.RemoveRange(account.Transactions);
            await _unitOfWork.CompleteAsync();

            _unitOfWork.Account.Remove(account);
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
