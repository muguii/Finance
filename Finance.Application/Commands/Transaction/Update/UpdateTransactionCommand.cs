using MediatR;

namespace Finance.Application.Commands.Transaction.Update
{
    public class UpdateTransactionCommand : IRequest<Unit>
    {
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public int TransactionId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
