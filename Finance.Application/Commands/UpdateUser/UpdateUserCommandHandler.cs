using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.User.GetByIdAsync(request.UserId);

            user.Update(request.Name, request.LastName, request.Telephone,
                        request.Street, request.Number, request.PostalCode, request.District, request.City, request.State, request.Country);

            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
