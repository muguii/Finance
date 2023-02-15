using Finance.Core.Entities;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(request.Description, request.Color, request.Type, request.UserId);

            await _unitOfWork.Category.AddAsync(category);
            await _unitOfWork.CompleteAsync();

            return category.Id;
        }
    }
}
