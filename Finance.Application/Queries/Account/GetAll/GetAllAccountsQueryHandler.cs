using Finance.Application.Mappers.Account;
using Finance.Application.ViewModels.Account;
using Finance.Core.Models;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Queries.Account.GetAll
{
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, PaginationResult<AccountViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAccountsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResult<AccountViewModel>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var paginationAccounts = await _unitOfWork.Account.GetAllAsync(request.Query, request.Page);
            var accounts = paginationAccounts.Data.Select(account => account.ToAccountViewModel()).ToList();

            return new PaginationResult<AccountViewModel>(paginationAccounts.Page,
                                                          paginationAccounts.TotalPages,
                                                          paginationAccounts.PageSize,
                                                          paginationAccounts.ItemsCount,
                                                          accounts);
        }
    }
}
