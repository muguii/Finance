using Finance.Application.Mappers;
using Finance.Application.ViewModels;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Queries.Category.GetAll
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CategoryViewModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Category.GetAllAsync(request.Query);
            return categories.Select(category => category.ToCategoryViewModel()).ToList();
        }
    }
}
