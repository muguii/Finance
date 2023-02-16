using MediatR;

namespace Finance.Application.Commands.Account.Delete
{
    public class DeleteAccountCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteAccountCommand(int id)
        {
            Id = id;
        }
    }
}
