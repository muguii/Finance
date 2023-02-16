using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.Category.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Core.Entities.Category(request.Description, request.Color, request.Type, request.UserId);

            await _unitOfWork.Category.AddAsync(category);
            await _unitOfWork.CompleteAsync();

            return category.Id;
        }
    }
}
