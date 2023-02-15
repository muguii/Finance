using MediatR;

namespace Finance.Application.Commands.UnshelveCategory
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
