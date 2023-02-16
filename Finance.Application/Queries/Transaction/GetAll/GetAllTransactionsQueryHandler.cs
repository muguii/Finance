using Finance.Application.Mappers.Transaction;
using Finance.Application.ViewModels.Transaction;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Queries.Transaction.GetAll
{
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, List<TransactionDetailsViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTransactionsQueryHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<List<TransactionDetailsViewModel>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _unitOfWork.Transaction.GetAllAsync(request.Query);
            return transactions.Select(transaction => transaction.ToTransactionDetailsViewModel()).ToList();
        }
    }
}
