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

            //if (transaction == null)
                //Exception?

            _unitOfWork.Transaction.Remove(transaction);
            await _unitOfWork.CompleteAsync();

            // TODO: Add or decrease the value of the account

            return Unit.Value;
        }
    }
}
