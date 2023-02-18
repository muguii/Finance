using Finance.Application.Commands.Category.Update;
using FluentValidation;

namespace Finance.Application.Validators.Category
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(c => c.CategoryId)
                .NotEmpty()
                .NotNull();

            RuleFor(c => c.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(64)
                .WithErrorCode("Category description can be a maximum of 64 characters.");

            RuleFor(c => c.Color)
                .NotEmpty()
                .NotNull()
                .MaximumLength(32)
                .WithErrorCode("Category color can be a maximum of 32 characters.");
        }
    }
}
