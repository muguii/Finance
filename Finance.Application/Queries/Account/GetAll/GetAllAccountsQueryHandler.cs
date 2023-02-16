using Finance.Application.Mappers;
using Finance.Application.ViewModels;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Queries.Account.GetAll
{
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAccountsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AccountViewModel>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _unitOfWork.Account.GetAllAsync(request.Query);
            return accounts.Select(account => account.ToAccountViewModel()).ToList();
        }
    }
}
