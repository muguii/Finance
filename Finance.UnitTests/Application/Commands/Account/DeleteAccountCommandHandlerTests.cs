using Finance.Application.Commands.Account.Delete;
using Finance.Core.Exceptions;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Commands.Account
{
    public class DeleteAccountCommandHandlerTests
    {
        [Fact]
        public async void AccountWithIdFiveExists_Executed_DeleteAccountWithIdFive() // GIVEN_WHEN_THEN
        {
            // Arrange
            var accountIdMock = 5;
            var accountMock = new Finance.Core.Entities.Account("Description 1", "Color 1", 100, 1);

            var accountRepositoryMock = new Mock<IAccountRepository>();
            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            accountRepositoryMock.Setup(ar => ar.GetByIdWithDetailsAsync(It.Is<int>(id => id == accountIdMock))).ReturnsAsync(accountMock);
            unitOfWorkMock.SetupGet(uw => uw.Account).Returns(accountRepositoryMock.Object);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var deleteAccountCommand = new DeleteAccountCommand(accountIdMock);
            var deleteAccountCommandHandler = new DeleteAccountCommandHandler(unitOfWorkMock.Object);

            // Act
            await deleteAccountCommandHandler.Handle(deleteAccountCommand, new CancellationToken());

            // Assert
            transactionRepositoryMock.Verify(ar => ar.RemoveRange(It.IsAny<List<Finance.Core.Entities.Transaction>>()), Times.Once);
            accountRepositoryMock.Verify(ar => ar.Remove(It.IsAny<Finance.Core.Entities.Account>()), Times.Once);
            unitOfWorkMock.Verify(uw => uw.BeginTransactionAsync(), Times.Once);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.AtLeastOnce);
            unitOfWorkMock.Verify(uw => uw.CommitAsync(), Times.Once);
        }

        [Fact]
        public async void AccountWithIdSevenNotExists_Executed_ThrowAccountNotExistsException() // GIVEN_WHEN_THEN
        {
            // Arrange
            var accountIdMock = 7;
            Finance.Core.Entities.Account accountMock = null;

            var accountRepositoryMock = new Mock<IAccountRepository>();
            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            accountRepositoryMock.Setup(ar => ar.GetByIdWithDetailsAsync(It.Is<int>(id => id == accountIdMock))).ReturnsAsync(accountMock);
            unitOfWorkMock.SetupGet(uw => uw.Account).Returns(accountRepositoryMock.Object);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var deleteAccountCommand = new DeleteAccountCommand(accountIdMock);
            var deleteAccountCommandHandler = new DeleteAccountCommandHandler(unitOfWorkMock.Object);

            // Act
            var act = () => deleteAccountCommandHandler.Handle(deleteAccountCommand, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<AccountNotExistsException>(act);

            accountRepositoryMock.Verify(ar => ar.GetByIdWithDetailsAsync(It.Is<int>(id => id == accountIdMock)), Times.Once);
            accountRepositoryMock.Verify(ar => ar.GetByIdWithDetailsAsync(It.Is<int>(id => id != accountIdMock)), Times.Never);

            transactionRepositoryMock.Verify(ar => ar.RemoveRange(It.IsAny<List<Finance.Core.Entities.Transaction>>()), Times.Never);
            accountRepositoryMock.Verify(ar => ar.Remove(It.IsAny<Finance.Core.Entities.Account>()), Times.Never);
            unitOfWorkMock.Verify(uw => uw.BeginTransactionAsync(), Times.Never);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.Never);
            unitOfWorkMock.Verify(uw => uw.CommitAsync(), Times.Never);
        }
    }
}
