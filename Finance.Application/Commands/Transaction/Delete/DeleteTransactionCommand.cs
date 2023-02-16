using MediatR;

namespace Finance.Application.Commands.Transaction.Delete
{
    public class DeleteTransactionCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteTransactionCommand(int id)
        {
            Id = id;
        }
    }
}
