using Finance.Application.ViewModels;
using MediatR;

namespace Finance.Application.Queries.User.GetById
{
    public class GetUserByIdQuery : IRequest<UserDetailsViewModel>
    {
        public int Id { get; private set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
