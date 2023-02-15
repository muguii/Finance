using Finance.Application.ViewModels;
using MediatR;

namespace Finance.Application.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<CategoryDetailsViewModel>
    {
        public int Id { get; set; }

        public GetCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
