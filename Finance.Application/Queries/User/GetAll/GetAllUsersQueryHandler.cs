using Finance.Application.Mappers;
using Finance.Application.ViewModels;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Queries.User.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.User.GetAllAsync(request.Query);
            return users.Select(user => user.ToUserViewModel()).ToList();
        }
    }
}
