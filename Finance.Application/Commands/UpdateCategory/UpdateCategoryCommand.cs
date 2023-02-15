using MediatR;

namespace Finance.Application.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<Unit>
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}
