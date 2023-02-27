using Finance.Application.Mappers.User;
using Finance.Application.ViewModels.User;
using Finance.Core.Models;
using Finance.Infrastructure.Persistence;
using MediatR;

namespace Finance.Application.Queries.User.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginationResult<UserViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResult<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var paginationUsers = await _unitOfWork.User.GetAllAsync(request.Query, request.Page);
            var users = paginationUsers.Data.Select(user => user.ToUserViewModel()).ToList();

            return new PaginationResult<UserViewModel>(paginationUsers.Page,
                                                       paginationUsers.TotalPages,
                                                       paginationUsers.PageSize,
                                                       paginationUsers.ItemsCount,
                                                       users);
        }
    }
}
