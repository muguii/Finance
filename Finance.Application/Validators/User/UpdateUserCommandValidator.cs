using Finance.Application.Commands.User.Update;
using FluentValidation;

namespace Finance.Application.Validators.User
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(u => u.UserId)
                .NotEmpty()
                .NotNull();

            RuleFor(u => u.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(64)
                .WithMessage("User name can be a maximum of 64 characters.");

            RuleFor(u => u.LastName)
                .MaximumLength(128)
                .WithMessage("User last name can be a maximum of 128 characters.");

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
        }
    }
}