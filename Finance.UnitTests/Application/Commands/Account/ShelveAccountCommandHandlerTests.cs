using Finance.Application.Commands.Account.Shelve;
using Finance.Core.Exceptions;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Commands.Account
{
    public class ShelveAccountCommandHandlerTests
    {
        [Fact]
        public async void AccountWithIdFourExists_Executed_DisableAccountWithIdFour() // GIVEN_WHEN_THEN
        {
            // Arrange
            var accountIdMock = 4;
            var accountMock = new Finance.Core.Entities.Account("Description 1", "Color 1", 100, 1);

            var accountRepositoryMock = new Mock<IAccountRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            accountRepositoryMock.Setup(ar => ar.GetByIdAsync(It.Is<int>(id => id == accountIdMock))).ReturnsAsync(accountMock);
            unitOfWorkMock.SetupGet(uw => uw.Account).Returns(accountRepositoryMock.Object);

            var shelveAccountCommand = new ShelveAccountCommand(accountIdMock);
            var shelveAccountCommandHandler = new ShelveAccountCommandHandler(unitOfWorkMock.Object);

            Assert.True(accountMock.Active);

            // Act
            await shelveAccountCommandHandler.Handle(shelveAccountCommand, new CancellationToken());

            // Assert
            Assert.False(accountMock.Active);

            accountRepositoryMock.Verify(ar => ar.GetByIdAsync(It.Is<int>(id => id == accountIdMock)), Times.Once);
            accountRepositoryMock.Verify(ar => ar.GetByIdAsync(It.Is<int>(id => id != accountIdMock)), Times.Never);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.AtLeastOnce);
        }

        [Fact]
        public async void AccountWithIdSevenNotExists_Executed_ThrowAccountNotExistsException() // GIVEN_WHEN_THEN
        {
            // Arrange
            var accountIdMock = 7;
            Finance.Core.Entities.Account accountMock = null;

            var accountRepositoryMock = new Mock<IAccountRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            accountRepositoryMock.Setup(ar => ar.GetByIdAsync(It.Is<int>(id => id == accountIdMock))).ReturnsAsync(accountMock);
            unitOfWorkMock.SetupGet(uw => uw.Account).Returns(accountRepositoryMock.Object);

            var shelveAccountCommand = new ShelveAccountCommand(accountIdMock);
            var shelveAccountCommandHandler = new ShelveAccountCommandHandler(unitOfWorkMock.Object);

            // Act
            var act = () => shelveAccountCommandHandler.Handle(shelveAccountCommand, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<AccountNotExistsException>(act);

            accountRepositoryMock.Verify(ar => ar.GetByIdAsync(It.Is<int>(id => id == accountIdMock)), Times.Once);
            accountRepositoryMock.Verify(ar => ar.GetByIdAsync(It.Is<int>(id => id != accountIdMock)), Times.Never);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.Never);
        }
    }
}
