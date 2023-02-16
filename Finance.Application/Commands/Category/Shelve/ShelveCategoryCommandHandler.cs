using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.Category.Shelve
{
    public class ShelveCategoryCommandHandler : IRequestHandler<ShelveCategoryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShelveCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ShelveCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(request.Id);

            //if (category == null)
            // Exception?

            category.Shelve();
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
