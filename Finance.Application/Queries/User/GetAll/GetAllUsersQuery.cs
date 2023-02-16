using Finance.Application.ViewModels.User;
using MediatR;

namespace Finance.Application.Queries.User.GetAll
{
    public class GetAllUsersQuery : IRequest<List<UserViewModel>>
    {
        public string Query { get; set; }
    }
}
