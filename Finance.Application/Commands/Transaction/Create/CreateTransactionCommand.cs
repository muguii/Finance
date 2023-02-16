using MediatR;

namespace Finance.Application.Commands.Transaction.Create
{
    public class CreateTransactionCommand : IRequest<int>
    {
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
