using MediatR;

namespace Finance.Application.Commands.Category.Shelve
{
    public class ShelveCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public ShelveCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
