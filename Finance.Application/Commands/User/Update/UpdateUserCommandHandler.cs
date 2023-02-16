using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Commands.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.User.GetByIdAsync(request.UserId);

            //if (user == null)
            // Exception?

            user.Update(request.Name, request.LastName, request.Telephone,
                        request.Street, request.Number, request.PostalCode, request.District, request.City, request.State, request.Country);

            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
