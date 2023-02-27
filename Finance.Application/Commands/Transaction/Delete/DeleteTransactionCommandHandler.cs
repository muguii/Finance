using Finance.Core.Enums;
using Finance.Core.Exceptions;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.Transaction.Delete
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTransactionCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.Transaction.GetByIdAsync(request.Id);

            if (transaction == null)
                throw new TransactionNotExistsException(request.Id);

            await _unitOfWork.BeginTransactionAsync();

            if (transaction.Category.Type == CategoryType.Expense)
                transaction.Account.Credit(transaction.Value);
            else
                transaction.Account.Debit(transaction.Value);

            _unitOfWork.Transaction.Remove(transaction);

            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
