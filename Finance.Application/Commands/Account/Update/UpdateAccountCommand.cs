using MediatR;

namespace Finance.Application.Commands.Account.Update
{
    public class UpdateAccountCommand : IRequest<Unit>
    {
        public int AccountId { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal Balance { get; set; }
    }
}
