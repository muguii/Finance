using Finance.Application.ViewModels;
using MediatR;

namespace Finance.Application.Queries.Account.GetById
{
    public class GetAccountByIdQuery : IRequest<AccountDetailsViewModel>
    {
        public int Id { get; private set; }

        public GetAccountByIdQuery(int id)
        {
            Id = id;
        }
    }
}
