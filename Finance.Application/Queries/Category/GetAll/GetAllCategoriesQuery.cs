using Finance.Application.ViewModels;
using MediatR;

namespace Finance.Application.Queries.Category.GetAll
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryViewModel>>
    {
        public string Query { get; set; }
    }
}
