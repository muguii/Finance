using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.Transaction.Create
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTransactionCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new Core.Entities.Transaction(request.Description, request.Date, request.Value, request.AccountId, request.CategoryId);

            await _unitOfWork.Transaction.AddAsync(transaction);
            await _unitOfWork.CompleteAsync();

            // TODO: Increase or decrease account balance

            return transaction.Id;
        }
    }
}
