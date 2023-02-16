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

            //if (transaction == null)
                //Exception?

            transaction.Update(request.CategoryId, request.AccountId, request.Description, request.Date);
            await _unitOfWork.CompleteAsync();

            // TODO: When changing the account, add the value in old account and decrease value in new account

            return Unit.Value;
        }
    }
}
