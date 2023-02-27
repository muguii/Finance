using Finance.Core.Exceptions;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.Account.Unshelve
{
    public class UnshelveAccountCommandHandler : IRequestHandler<UnshelveAccountCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnshelveAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UnshelveAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.Account.GetByIdAsync(request.Id);

            if (account == null)
                throw new AccountNotExistsException(request.Id);

            account.Unshelve();
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
