using Finance.Application.ViewModels.Account;
using MediatR;

namespace Finance.Application.Queries.Account.GetAll
{
    public class GetAllAccountsQuery : IRequest<List<AccountViewModel>>
    {
        public string Query { get; set; }
    }
}
