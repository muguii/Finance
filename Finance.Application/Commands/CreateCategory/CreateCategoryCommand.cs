using Finance.Core.Enums;
using MediatR;

namespace Finance.Application.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public CategoryType Type { get; set; }
    }
}
