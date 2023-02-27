using Finance.Application.ViewModels.Category;
using Finance.Core.Models;
using MediatR;

namespace Finance.Application.Queries.Category.GetAll
{
    public class GetAllCategoriesQuery : IRequest<PaginationResult<CategoryViewModel>>
    {
        public string? Query { get; set; }
        public int Page { get; set; } = 1;
    }
}
