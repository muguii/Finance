using MediatR;

namespace Finance.Application.Commands.Account.Create
{
    public class CreateAccountCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal InitialBalance { get; set; }
    }
}
