using Finance.Application.Mappers.Account;
using Finance.Application.ViewModels;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Queries.Account.GetById
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDetailsViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AccountDetailsViewModel> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.Account.GetByIdWithDetailsAsync(request.Id);

            if (account == null)
                return null;

            return account.ToAccountDetailsViewModel();
        }
    }
}
