using Finance.Application.Queries.Transaction.GetAll;
using Finance.Application.Queries.User.GetAll;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Queries.User
{
    public class GetAllUsersQueryHandlerTests
    {
        [Fact]
        public async void TwoUserExists_Executed_ReturnTwoUserViewModels() // GIVEN_WHEN_THEN
        {
            // Arrange
            var paginationUserMock = new PaginationResult<Finance.Core.Entities.User>
            {
                Data = new List<Finance.Core.Entities.User>
                {
                    new Finance.Core.Entities.User("Login 1", "Password 1", "Email 1", "Name 1", "Last Name 1", DateTime.Now, "Gender 1", "Telephone 1", "Role 1"),
                    new Finance.Core.Entities.User("Login 2", "Password 2", "Email 2", "Name 2", "Last Name 2", DateTime.Now, "Gender 2", "Telephone 2", "Role 2")
                }
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            userRepositoryMock.Setup(tr => tr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(paginationUserMock);
            unitOfWorkMock.SetupGet(uw => uw.User).Returns(userRepositoryMock.Object);

            var getAllUsersQuery = new GetAllUsersQuery();
            var getAllUsersQueryHandler = new GetAllUsersQueryHandler(unitOfWorkMock.Object);

            // Act
            var paginationUserDetails = await getAllUsersQueryHandler.Handle(getAllUsersQuery, new CancellationToken());

            // Assert
            Assert.NotNull(paginationUserDetails);
            Assert.NotNull(paginationUserDetails.Data);
            Assert.NotEmpty(paginationUserDetails.Data);
            Assert.Equal(paginationUserMock.Data.Count, paginationUserDetails.Data.Count);

            userRepositoryMock.Verify(tr => tr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }
    }
}
