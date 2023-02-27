using Finance.Application.Mappers.Category;
using Finance.Application.ViewModels.Category;
using Finance.Core.Models;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Queries.Category.GetAll
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, PaginationResult<CategoryViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResult<CategoryViewModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var paginationCategories = await _unitOfWork.Category.GetAllAsync(request.Query, request.Page);
            var categories = paginationCategories.Data.Select(category => category.ToCategoryViewModel()).ToList();

            return new PaginationResult<CategoryViewModel>(paginationCategories.Page, 
                                                           paginationCategories.TotalPages,
                                                           paginationCategories.PageSize,
                                                           paginationCategories.ItemsCount, 
                                                           categories);
        }
    }
}
