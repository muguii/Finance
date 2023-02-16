using Finance.Application.ViewModels.Transaction;
using MediatR;

namespace Finance.Application.Queries.Transaction.GetAll
{
    public class GetAllTransactionsQuery : IRequest<List<TransactionDetailsViewModel>>
    {
        public string Query { get; set; }
    }
}
