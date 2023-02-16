using MediatR;

namespace Finance.Application.Commands.User.Create
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Gender { get; set; }
        public string Telephone { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Role { get; set; }
    }
}
