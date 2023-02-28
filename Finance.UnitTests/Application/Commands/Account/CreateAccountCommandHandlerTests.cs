using Finance.Application.Commands.Account.Create;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Commands.Account
{
    public class CreateAccountCommandHandlerTests
    {
        [Fact]
        public async void InputDataIsOk_Executed_ReturnAccountId() // GIVEN_WHEN_THEN
        {
            // Arrange
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            unitOfWorkMock.SetupGet(uw => uw.Account).Returns(accountRepositoryMock.Object);

            var createAccountCommand = new CreateAccountCommand()
            {
                Description = "Description 1",
                Color = "Color 1",
                InitialBalance = 100,
                UserId = 1
            };
            var createAccountCommandHandler = new CreateAccountCommandHandler(unitOfWorkMock.Object);

            // Act
            var accountId = await createAccountCommandHandler.Handle(createAccountCommand, new CancellationToken());

            // Assert
            Assert.True(accountId >= 0);

            accountRepositoryMock.Verify(ar => ar.AddAsync(It.IsAny<Finance.Core.Entities.Account>()), Times.Once);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.AtLeastOnce);
        }
    }
}
