using Finance.Core.Enums;
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

            bool valueChanged = request.Value != transaction.Value;
            if (valueChanged)
            {
                const int positiveValue = -1;
                decimal adjustmentValue = request.Value - transaction.Value;

                if (transaction.Category.Type == CategoryType.Expense)
                {
                    bool expenseIncreased = adjustmentValue >= 0;
                    if (expenseIncreased)
                        transaction.Account.Debit(adjustmentValue);
                    else
                        transaction.Account.Credit(adjustmentValue * positiveValue);
                }
                else
                {
                    bool incomeIncreased = adjustmentValue >= 0;
                    if (incomeIncreased)
                        transaction.Account.Credit(adjustmentValue);
                    else
                        transaction.Account.Debit(adjustmentValue * positiveValue);
                }
            }

            transaction.Update(request.Description, request.Date, request.Value);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
