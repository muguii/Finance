using Finance.Application.Mappers.Transaction;
using Finance.Application.ViewModels.Transaction;
using Finance.Core.Models;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Queries.Transaction.GetAll
{
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, PaginationResult<TransactionDetailsViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTransactionsQueryHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<PaginationResult<TransactionDetailsViewModel>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var paginationTransactions = await _unitOfWork.Transaction.GetAllAsync(request.Query, request.Page);
            var transactions = paginationTransactions.Data.Select(transaction => transaction.ToTransactionDetailsViewModel()).ToList();

            return new PaginationResult<TransactionDetailsViewModel>(paginationTransactions.Page,
                                                                     paginationTransactions.TotalPages,
                                                                     paginationTransactions.PageSize,
                                                                     paginationTransactions.ItemsCount,
                                                                     transactions);
        }
    }
}
