using Finance.Core.Exceptions;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.Category.Unshelve
{
    public class UnshelveCategoryCommandHandler : IRequestHandler<UnshelveCategoryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnshelveCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UnshelveCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(request.Id);

            if (category == null)
                throw new CategoryNotExistsException(request.Id);

            category.Unshelve();
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
