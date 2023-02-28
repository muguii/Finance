using Finance.Application.Commands.User.Login;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Finance.Infrastructure.Services.Auth;
using Moq;

namespace Finance.UnitTests.Application.Commands.User
{
    public class LoginUserCommandHandlerTests
    {
        [Fact]
        public async void LoginDataIsOk_Executed_GenerateJwtTokenAndReturnLoginUserViewModel() // GIVEN_WHEN_THEN
        {
            // Arrange
            var loginMock = "Login 1";
            var emailMock = "Email 1";
            var passwordMock = "111111";
            var passwordHashMock = "123321";
            var jwtTokenMock = "jwtToken111";

            var userMock = new Finance.Core.Entities.User(loginMock, passwordMock, emailMock, "Name 1", "Last Name 1", DateTime.Now, "Gender 1", "Telephone 1", "Role 1");

            var authServiceMock = new Mock<IAuthService>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            authServiceMock.Setup(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw == passwordMock))).Returns(passwordHashMock);
            userRepositoryMock.Setup(ur => ur.GetByLoginAndPasswordAsync(It.Is<string>(login => login == loginMock), It.Is<string>(pwHash => pwHash == passwordHashMock))).ReturnsAsync(userMock);
            authServiceMock.Setup(auth => auth.GenerateJwtToken(It.Is<string>(login => login == loginMock), It.IsAny<string>(), It.IsAny<string>())).Returns(jwtTokenMock);
            unitOfWorkMock.SetupGet(uw => uw.User).Returns(userRepositoryMock.Object);

            var loginUserCommand = new LoginUserCommand()
            {
                Login = loginMock,
                Password = passwordMock,
            };
            var loginUserCommandHandler = new LoginUserCommandHandler(unitOfWorkMock.Object, authServiceMock.Object);

            // Act
            var userLoginModel = await loginUserCommandHandler.Handle(loginUserCommand, new CancellationToken());

            // Assert
            Assert.NotNull(userLoginModel);
            Assert.Equal(loginMock, userLoginModel.Login);
            Assert.Equal(emailMock, userLoginModel.Email);
            Assert.Equal(jwtTokenMock, userLoginModel.Token);

            userRepositoryMock.Verify(ur => ur.GetByLoginAndPasswordAsync(It.Is<string>(login => login == loginMock), It.Is<string>(pwHash => pwHash == passwordHashMock)), Times.Once);
            userRepositoryMock.Verify(ur => ur.GetByLoginAndPasswordAsync(It.Is<string>(login => login != loginMock), It.Is<string>(pwHash => pwHash == passwordHashMock)), Times.Never);
            userRepositoryMock.Verify(ur => ur.GetByLoginAndPasswordAsync(It.Is<string>(login => login == loginMock), It.Is<string>(pwHash => pwHash != passwordHashMock)), Times.Never);
            userRepositoryMock.Verify(ur => ur.GetByLoginAndPasswordAsync(It.Is<string>(login => login != loginMock), It.Is<string>(pwHash => pwHash != passwordHashMock)), Times.Never);

            authServiceMock.Verify(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw == loginUserCommand.Password)), Times.Once);
            authServiceMock.Verify(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw != loginUserCommand.Password)), Times.Never);
            authServiceMock.Verify(auth => auth.GenerateJwtToken(It.Is<string>(login => login == loginMock), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            authServiceMock.Verify(auth => auth.GenerateJwtToken(It.Is<string>(login => login != loginMock), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async void LoginDataIsOkAndUserNotExists_Executed_ReturnNull() // GIVEN_WHEN_THEN
        {
            // Arrange
            var loginMock = "Login 1";
            var passwordMock = "111111";
            var passwordHashMock = "123321";

            Finance.Core.Entities.User userMock = null;

            var authServiceMock = new Mock<IAuthService>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            authServiceMock.Setup(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw == passwordMock))).Returns(passwordHashMock);
            userRepositoryMock.Setup(ur => ur.GetByLoginAndPasswordAsync(It.Is<string>(login => login == loginMock), It.Is<string>(pwHash => pwHash == passwordHashMock))).ReturnsAsync(userMock);
            unitOfWorkMock.SetupGet(uw => uw.User).Returns(userRepositoryMock.Object);

            var loginUserCommand = new LoginUserCommand()
            {
                Login = loginMock,
                Password = passwordMock,
            };
            var loginUserCommandHandler = new LoginUserCommandHandler(unitOfWorkMock.Object, authServiceMock.Object);

            // Act
            var userLoginModel = await loginUserCommandHandler.Handle(loginUserCommand, new CancellationToken());

            // Assert
            Assert.Null(userLoginModel);

            userRepositoryMock.Verify(ur => ur.GetByLoginAndPasswordAsync(It.Is<string>(login => login == loginMock), It.Is<string>(pwHash => pwHash == passwordHashMock)), Times.Once);
            userRepositoryMock.Verify(ur => ur.GetByLoginAndPasswordAsync(It.Is<string>(login => login != loginMock), It.Is<string>(pwHash => pwHash == passwordHashMock)), Times.Never);
            userRepositoryMock.Verify(ur => ur.GetByLoginAndPasswordAsync(It.Is<string>(login => login == loginMock), It.Is<string>(pwHash => pwHash != passwordHashMock)), Times.Never);
            userRepositoryMock.Verify(ur => ur.GetByLoginAndPasswordAsync(It.Is<string>(login => login != loginMock), It.Is<string>(pwHash => pwHash != passwordHashMock)), Times.Never);

            authServiceMock.Verify(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw == loginUserCommand.Password)), Times.Once);
            authServiceMock.Verify(auth => auth.ComputeSha256Hash(It.Is<string>(pw => pw != loginUserCommand.Password)), Times.Never);

            authServiceMock.Verify(auth => auth.GenerateJwtToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
