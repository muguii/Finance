using MediatR;

namespace Finance.Application.Commands.Account.Unshelve
{
    public class UnshelveAccountCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public UnshelveAccountCommand(int id)
        {
            Id = id;
        }
    }
}
