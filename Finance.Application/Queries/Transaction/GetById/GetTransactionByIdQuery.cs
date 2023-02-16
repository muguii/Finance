using Finance.Application.ViewModels.Transaction;
using MediatR;

namespace Finance.Application.Queries.Transaction.GetById
{
    public class GetTransactionByIdQuery : IRequest<TransactionDetailsViewModel>
    {
        public int Id { get; set; }

        public GetTransactionByIdQuery(int id)
        {
            Id = id;
        }
    }
}
