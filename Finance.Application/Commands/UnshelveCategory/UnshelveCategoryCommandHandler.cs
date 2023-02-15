using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.UnshelveCategory
{
    public class UnshelveCategoryCommandHandler : IRequestHandler<UnshelveCategoryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnshelveCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UnshelveCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(request.Id);

            //if (category == null)
            // Exception?

            category.Unshelve();
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
