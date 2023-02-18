using Finance.Application.Commands.Category.Create;
using FluentValidation;

namespace Finance.Application.Validators.Category
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty()
                .NotNull();

            RuleFor(c => c.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(64)
                .WithErrorCode("Category description can be a maximum of 64 characters.");

            RuleFor(c => c.Color)
                .NotNull()
                .NotEmpty()
                .MaximumLength(32)
                .WithErrorCode("Category color can be a maximum of 32 characters.");

            RuleFor(c => c.Type)
                .NotEmpty()
                .NotNull()
                .IsInEnum()
                .WithMessage("Category type invalid.");
        }
    }
}
