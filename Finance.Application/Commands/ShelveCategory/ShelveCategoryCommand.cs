using MediatR;

namespace Finance.Application.Commands.ShelveCategory
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
