using MediatR;

namespace Finance.Application.Commands.Account.Shelve
{
    public class ShelveAccountCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public ShelveAccountCommand(int id)
        {
            Id = id;
        }
    }
}
