using Finance.Application.Mappers.Transaction;
using Finance.Application.ViewModels.Transaction;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Queries.Transaction.GetById
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDetailsViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTransactionByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<TransactionDetailsViewModel> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.Transaction.GetByIdAsync(request.Id);

            if (transaction == null)
                return null;

            return transaction.ToTransactionDetailsViewModel();
        }
    }
}
