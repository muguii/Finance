using MediatR;

namespace Finance.Application.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
