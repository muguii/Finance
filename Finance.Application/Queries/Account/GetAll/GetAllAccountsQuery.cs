using Finance.Application.ViewModels.Account;
using Finance.Core.Models;
using MediatR;

namespace Finance.Application.Queries.Account.GetAll
{
    public class GetAllAccountsQuery : IRequest<PaginationResult<AccountViewModel>>
    {
        public string? Query { get; set; }
        public int Page { get; set; } = 1;
    }
}
