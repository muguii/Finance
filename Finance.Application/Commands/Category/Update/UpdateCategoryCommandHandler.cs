using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.Category.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(request.CategoryId);

            //if (category == null)
            // Exception?

            category.Update(request.Description, request.Color);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
