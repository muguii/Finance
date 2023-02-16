using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.Category.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Category.GetByIdWithDetailsAsync(request.Id);

            //if (category == null)
            // Exception?

            //if (category.Active)
            // Can only delete categories that are disabled (Shelved)

            await _unitOfWork.BeginTransactionAsync();

            _unitOfWork.Transaction.RemoveRange(category.Transactions);
            await _unitOfWork.CompleteAsync();

            _unitOfWork.Category.Remove(category);
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
