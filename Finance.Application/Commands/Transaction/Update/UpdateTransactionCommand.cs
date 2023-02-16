using MediatR;

namespace Finance.Application.Commands.Transaction.Update
{
    public class UpdateTransactionCommand : IRequest<Unit>
    {
        public int TransactionId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
