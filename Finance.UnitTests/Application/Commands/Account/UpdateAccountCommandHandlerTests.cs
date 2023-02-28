using Finance.Application.Commands.Account.Update;
using Finance.Core.Exceptions;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Commands.Account
{
    public class UpdateAccountCommandHandlerTests
    {
        [Fact]
        public async void AccountWithIdFiveExistsAndInputDataIsOk_Executed_ChangeAccountData() // GIVEN_WHEN_THEN
        {
            // Arrange
            var accountIdMock = 5;
            var descriptionMock = "Description 1";
            var colorMock = "Color 1";
            var balanceMock = 100;

            var accountMock = new Finance.Core.Entities.Account(descriptionMock, colorMock, balanceMock, 1);

            var accountRepositoryMock = new Mock<IAccountRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            accountRepositoryMock.Setup(ar => ar.GetByIdAsync(It.Is<int>(id => id == accountIdMock))).ReturnsAsync(accountMock);
            unitOfWorkMock.SetupGet(uw => uw.Account).Returns(accountRepositoryMock.Object);

            var updateAccountCommand = new UpdateAccountCommand
            {
                Description = "New Description",
                Color = "New Color",
                Balance = 200,
                AccountId = accountIdMock
            };
            var updateAccountCommandHandler = new UpdateAccountCommandHandler(unitOfWorkMock.Object);

            Assert.Equal(accountMock.LastUpdate, accountMock.CreatedAt);

            // Act
            await updateAccountCommandHandler.Handle(updateAccountCommand, new CancellationToken());

            // Assert
            Assert.NotEqual(descriptionMock, accountMock.Description);
            Assert.NotEqual(colorMock, accountMock.Color);
            Assert.NotEqual(balanceMock, accountMock.Balance);
            Assert.NotEqual(accountMock.LastUpdate, accountMock.CreatedAt);

            accountRepositoryMock.Verify(ar => ar.GetByIdAsync(It.Is<int>(id => id == accountIdMock)), Times.Once);
            accountRepositoryMock.Verify(ar => ar.GetByIdAsync(It.Is<int>(id => id != accountIdMock)), Times.Never);

            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.AtLeastOnce);
        }

        [Fact]
        public async void AccountWithIdTwoNotExistsAndInputDataIsOk_Executed_ThrowAccountNotExistsException() // GIVEN_WHEN_THEN
        {
            // Arrange
            var accountIdMock = 2;
            Finance.Core.Entities.Account accountMock = null;

            var accountRepositoryMock = new Mock<IAccountRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            accountRepositoryMock.Setup(ar => ar.GetByIdAsync(It.Is<int>(id => id == accountIdMock))).ReturnsAsync(accountMock);
            unitOfWorkMock.SetupGet(uw => uw.Account).Returns(accountRepositoryMock.Object);

            var updateAccountCommand = new UpdateAccountCommand
            {
                Description = "New Description",
                Color = "New Color",
                Balance = 200,
                AccountId = accountIdMock
            };
            var updateAccountCommandHandler = new UpdateAccountCommandHandler(unitOfWorkMock.Object);

            // Act
            var act = () => updateAccountCommandHandler.Handle(updateAccountCommand, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<AccountNotExistsException>(act);

            accountRepositoryMock.Verify(ar => ar.GetByIdAsync(It.Is<int>(id => id == accountIdMock)), Times.Once);
            accountRepositoryMock.Verify(ar => ar.GetByIdAsync(It.Is<int>(id => id != accountIdMock)), Times.Never);

            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.Never);
        }
    }
}
