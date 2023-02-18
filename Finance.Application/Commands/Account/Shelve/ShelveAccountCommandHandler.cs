using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.Account.Shelve
{
    public class ShelveAccountCommandHandler : IRequestHandler<ShelveAccountCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShelveAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ShelveAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.Account.GetByIdAsync(request.Id);

            //if (account == null)
                // TODO: Exception?

            account.Shelve();
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
