using Finance.Application.ViewModels.Transaction;
using Finance.Core.Models;
using MediatR;

namespace Finance.Application.Queries.Transaction.GetAll
{
    public class GetAllTransactionsQuery : IRequest<PaginationResult<TransactionDetailsViewModel>>
    {
        public string? Query { get; set; }
        public int Page { get; set; } = 1;
    }
}
