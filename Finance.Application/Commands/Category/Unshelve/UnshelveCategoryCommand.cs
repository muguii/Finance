using MediatR;

namespace Finance.Application.Commands.Category.Unshelve
{
    public class UnshelveCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public UnshelveCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
