using Finance.Application.Commands.User.Create;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Finance.Infrastructure.Services.Auth;
using Moq;

namespace Finance.UnitTests.Application.Commands.User
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async void InputDataIsOk_Executed_ReturnUserId() // GIVEN_WHEN_THEN
        {
            // Arrange
            var passwordMock = "111111";
            var passwordHashMock = "123321";

            var authServiceMock = new Mock<IAuthService>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            authServiceMock.Setup(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw == passwordMock))).Returns(passwordHashMock);
            unitOfWorkMock.SetupGet(uw => uw.User).Returns(userRepositoryMock.Object);
            unitOfWorkMock.SetupGet(uw => uw.Account).Returns(accountRepositoryMock.Object);
            unitOfWorkMock.SetupGet(uw => uw.Category).Returns(categoryRepositoryMock.Object);

            var createUserCommand = new CreateUserCommand()
            {
                Login = "Login 1",
                Password = passwordMock,
                Email = "Email 1",
                Name = "Name 1",
                LastName = "Last Name 1",
                Birthdate = DateTime.Now,
                Gender = "Gender 1",
                Telephone = "Telephone 1",
                Role = "Role 1",
                Street = "Street 1",
                Number = "Number 1",
                PostalCode = "Postal Code 1",
                District = "Distric 1",
                City = "City 1",
                State = "State 1",
                Country = "Country 1"
            };
            var createUserCommandHandler = new CreateUserCommandHandler(unitOfWorkMock.Object, authServiceMock.Object);

            // Act
            var userId = await createUserCommandHandler.Handle(createUserCommand, new CancellationToken());

            // Assert
            Assert.True(userId >= 0);

            userRepositoryMock.Verify(ur => ur.AddAsync(It.IsAny<Finance.Core.Entities.User>()), Times.Once);
            userRepositoryMock.Verify(ur => ur.AddAddressAsync(It.IsAny<Finance.Core.Entities.Address>()), Times.Once);
            accountRepositoryMock.Verify(ur => ur.AddAsync(It.IsAny<Finance.Core.Entities.Account>()), Times.Once);
            categoryRepositoryMock.Verify(ur => ur.AddRangeAsync(It.IsAny<List<Finance.Core.Entities.Category>>()), Times.Once);

            unitOfWorkMock.Verify(uw => uw.BeginTransactionAsync(), Times.Once);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.AtLeastOnce);
            unitOfWorkMock.Verify(uw => uw.CommitAsync(), Times.Once);
        }
    }
}
