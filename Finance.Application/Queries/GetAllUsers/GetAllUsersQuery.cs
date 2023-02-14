using Finance.Application.ViewModels;
using MediatR;

namespace Finance.Application.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<UserViewModel>>
    {
        public string Query { get; set; }
    }
}
