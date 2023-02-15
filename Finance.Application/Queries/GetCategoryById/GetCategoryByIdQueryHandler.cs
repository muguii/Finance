using Finance.Application.Mappers;
using Finance.Application.ViewModels;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDetailsViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<CategoryDetailsViewModel> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(request.Id);

            if (category == null)
                return null;

            return category.ToCategoryDetailsViewModel();
        }
    }
}
