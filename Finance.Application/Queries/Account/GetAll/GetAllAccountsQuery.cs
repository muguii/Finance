using Finance.Application.ViewModels;
using MediatR;

namespace Finance.Application.Queries.Account.GetAll
{
    public class GetAllAccountsQuery : IRequest<List<AccountViewModel>>
    {
        public string Query { get; set; }
    }
}
