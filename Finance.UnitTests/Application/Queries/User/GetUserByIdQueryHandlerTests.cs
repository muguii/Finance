using Finance.Application.Queries.User.GetById;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Queries.User
{
    public class GetUserByIdQueryHandlerTests
    {
        [Fact]
        public async void UserWithIdFiveExists_Executed_ReturnUserDetailsViewModelOfUserFive() // GIVEN_WHEN_THEN
        {
            // Arrange
            var userIdMock = 5;

            var addressMock = new Finance.Core.Entities.Address(userIdMock, "Street 1", "Number 1", "PostalCode 1", "District 1", "City 1", "State 1", "Country 1");
            var userMock = new Finance.Core.Entities.User("Login 1", "Password 1", "Email 1", "Name 1", "Last Name 1", DateTime.Now, "Gender 1", "Telephone 1", "Role 1", addressMock);

            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            userRepositoryMock.Setup(tr => tr.GetByIdWithDetailsAsync(It.Is<int>(id => id == userIdMock))).ReturnsAsync(userMock);
            unitOfWorkMock.SetupGet(uw => uw.User).Returns(userRepositoryMock.Object);

            var getUserByIdQuery = new GetUserByIdQuery(userIdMock);
            var getUserByIdQueryHandler = new GetUserByIdQueryHandler(unitOfWorkMock.Object);

            // Act
            var userDetails = await getUserByIdQueryHandler.Handle(getUserByIdQuery, new CancellationToken());

            // Assert
            Assert.NotNull(userDetails);
            Assert.Equal(userMock.Login, userDetails.Login);
            Assert.Equal(userMock.Email, userDetails.Email);
            Assert.Contains(userMock.Name, userDetails.FullName);
            Assert.Contains(userMock.LastName, userDetails.FullName);
            Assert.Equal(userMock.Birthdate, userDetails.Birthdate);
            Assert.Equal(userMock.Gender, userDetails.Gender);
            Assert.Equal(userMock.Telephone, userDetails.Telephone);

            userRepositoryMock.Verify(tr => tr.GetByIdWithDetailsAsync(It.Is<int>(id => id == userIdMock)), Times.Once);
            userRepositoryMock.Verify(tr => tr.GetByIdWithDetailsAsync(It.Is<int>(id => id != userIdMock)), Times.Never);
        }

        [Fact]
        public async void UserWithIdTwoNotExists_Executed_ReturnNull() // GIVEN_WHEN_THEN
        {
            // Arrange
            var userIdMock = 5;
            Finance.Core.Entities.User userMock = null;

            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            userRepositoryMock.Setup(tr => tr.GetByIdWithDetailsAsync(It.Is<int>(id => id == userIdMock))).ReturnsAsync(userMock);
            unitOfWorkMock.SetupGet(uw => uw.User).Returns(userRepositoryMock.Object);

            var getUserByIdQuery = new GetUserByIdQuery(userIdMock);
            var getUserByIdQueryHandler = new GetUserByIdQueryHandler(unitOfWorkMock.Object);

            // Act
            var userDetails = await getUserByIdQueryHandler.Handle(getUserByIdQuery, new CancellationToken());

            // Assert
            Assert.Null(userDetails);

            userRepositoryMock.Verify(tr => tr.GetByIdWithDetailsAsync(It.Is<int>(id => id == userIdMock)), Times.Once);
            userRepositoryMock.Verify(tr => tr.GetByIdWithDetailsAsync(It.Is<int>(id => id != userIdMock)), Times.Never);
        }
    }
}
