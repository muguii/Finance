using Finance.Application.Commands.Account.Create;
using FluentValidation;

namespace Finance.Application.Validators.Account
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(a => a.UserId)
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(64)
                .WithErrorCode("Account description can be a maximum of 64 characters.");

            RuleFor(a => a.Color)
                .NotEmpty()
                .NotNull()
                .MaximumLength(32)
                .WithErrorCode("Account color can be a maximum of 32 characters.");

            RuleFor(a => a.InitialBalance)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Account initial balance cant be negative.");
        }
    }
}
