using Finance.Core.Exceptions;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.Transaction.Update
{
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTransactionCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.Transaction.GetByIdAsync(request.TransactionId);

            if (transaction == null)
                throw new TransactionNotExistsException(request.TransactionId);

            await _unitOfWork.BeginTransactionAsync();

            transaction.Update(request.Description, request.Date, request.Value);
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            // TODO: Changing account

            return Unit.Value;
        }
    }
}
