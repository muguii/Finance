using Finance.Application.Commands.User.Create;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Finance.Application.Validators.User
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.Login)
                .NotEmpty()
                .NotNull()
                .MaximumLength(64)
                .WithMessage("User login can be a maximum of 64 characters.");

            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull()
                .Must(ValidPassword)
                .WithMessage("User password must contain at least 8 characters, 1 number, 1 uppercase letter, 1 lowercase letter and 1 special character.");

            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .MaximumLength(64)
                .WithMessage("User email can be a maximum of 64 characters.")
                .EmailAddress()
                .WithMessage("Invalid email.");

            RuleFor(u => u.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(64)
                .WithMessage("User name can be a maximum of 64 characters.");

            RuleFor(u => u.LastName)
                .MaximumLength(128)
                .WithMessage("User last name can be a maximum of 128 characters.");

            RuleFor(u => u.Birthdate)
                .NotEmpty()
                .NotNull();

            RuleFor(u => u.Gender)
                .MaximumLength(16)
                .WithMessage("User gender can be a maximum of 16 characters.");

            RuleFor(u => u.Telephone)
                .NotEmpty()
                .NotNull()
                .MaximumLength(16)
                .WithMessage("User telephone can be a maximum of 16 characters.");

            RuleFor(u => u.Street)
                .NotEmpty()
                .NotNull()
                .MaximumLength(256)
                .WithMessage("User street can be a maximum of 256 characters.");

            RuleFor(u => u.Number)
                .NotEmpty()
                .NotNull()
                .MaximumLength(16)
                .WithMessage("User number can be a maximum of 16 characters.");

            RuleFor(u => u.PostalCode)
                .NotEmpty()
                .NotNull()
                .MaximumLength(16)
                .WithMessage("User postalCode can be a maximum of 16 characters.");

            RuleFor(u => u.District)
                .NotEmpty()
                .NotNull()
                .MaximumLength(64)
                .WithMessage("User district can be a maximum of 64 characters.");

            RuleFor(u => u.City)
                .NotEmpty()
                .NotNull()
                .MaximumLength(64)
                .WithMessage("User city can be a maximum of 64 characters.");

            RuleFor(u => u.State)
                .NotEmpty()
                .NotNull()
                .MaximumLength(64)
                .WithMessage("User state can be a maximum of 64 characters.");

            RuleFor(u => u.Country)
                .NotEmpty()
                .NotNull()
                .MaximumLength(64)
                .WithMessage("User country can be a maximum of 64 characters.");

            RuleFor(u => u.Role)
                .NotEmpty()
                .NotNull();
        }

        private bool ValidPassword(string password)
        {
            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");
            return regex.IsMatch(password);
        }
    }
}