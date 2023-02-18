using Finance.Application.Commands.Account.Update;
using FluentValidation;

namespace Finance.Application.Validators.Account
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            RuleFor(a => a.AccountId)
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

            RuleFor(a => a.Balance)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Account balance cant be negative.");
        }
    }
}
