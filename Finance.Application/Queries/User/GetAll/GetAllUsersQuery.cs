using Finance.Application.ViewModels.User;
using Finance.Core.Models;
using MediatR;

namespace Finance.Application.Queries.User.GetAll
{
    public class GetAllUsersQuery : IRequest<PaginationResult<UserViewModel>>
    {
        public string? Query { get; set; }
        public int Page { get; set; } = 1;
    }
}
