using Finance.Application.Commands.Transaction.Create;
using FluentValidation;

namespace Finance.Application.Validators.Transaction
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(t => t.AccountId)
                .NotEmpty()
                .NotNull();

            RuleFor(t => t.CategoryId)
                .NotEmpty()
                .NotNull();

            RuleFor(t => t.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(128)
                .WithErrorCode("Transaction description can be a maximum of 128 characters.");

            RuleFor(t => t.Date)
                .NotEmpty()
                .NotNull();

            RuleFor(t => t.Value)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Transaction value cant be negative.");
        }
    }
}