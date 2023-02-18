using Finance.Application.Commands.Transaction.Update;
using FluentValidation;

namespace Finance.Application.Validators.Transaction
{
    public class UpdateTransactionCommandValidator : AbstractValidator<UpdateTransactionCommand>
    {
        public UpdateTransactionCommandValidator()
        {
            RuleFor(t => t.TransactionId)
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
