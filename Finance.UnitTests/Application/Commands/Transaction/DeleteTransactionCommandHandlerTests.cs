using Finance.Application.Commands.Transaction.Delete;
using Finance.Core.Exceptions;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Commands.Transaction
{
    public class DeleteTransactionCommandHandlerTests
    {
        [Fact] 
        public async void TransactionWithIdFiveExistsAndTransactionTypeIsExpense_Executed_IncreaseAccountBalanceAndDeleteTransactionWithIdFive() // GIVEN_WHEN_THEN
        {
            // Arrange
            var transactionIdMock = 5;
            var transactionValueMock = 100;
            var accountBalanceMock = 100;
            var newAccountBalanceMock = accountBalanceMock + transactionValueMock;

            var accountMock = new Finance.Core.Entities.Account("Account Description 1", "Color 1", accountBalanceMock, 1);
            var categoryMock = new Finance.Core.Entities.Category("Category Description 1", "Color 1", Finance.Core.Enums.CategoryType.Expense, 1);
            var transactionMock = new Finance.Core.Entities.Transaction("Description 1", DateTime.Now, transactionValueMock, 1, 1, accountMock, categoryMock);

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            transactionRepositoryMock.Setup(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock))).ReturnsAsync(transactionMock);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var deleteTransactionCommand = new DeleteTransactionCommand(transactionIdMock);
            var deleteTransactionCommandHandler = new DeleteTransactionCommandHandler(unitOfWorkMock.Object);

            // Act
            await deleteTransactionCommandHandler.Handle(deleteTransactionCommand, new CancellationToken());

            // Assert
            Assert.Equal(accountMock.Balance, newAccountBalanceMock);

            transactionRepositoryMock.Verify(tr => tr.Remove(It.IsAny<Finance.Core.Entities.Transaction>()), Times.Once);
            unitOfWorkMock.Verify(uw => uw.BeginTransactionAsync(), Times.Once);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.AtLeastOnce);
            unitOfWorkMock.Verify(uw => uw.CommitAsync(), Times.Once);
        }

        [Fact]
        public async void TransactionWithIdFiveExistsAndTransactionTypeIsIncome_Executed_DecreaseAccountBalanceAndDeleteTransactionWithIdFive() // GIVEN_WHEN_THEN
        {
            // Arrange
            var transactionIdMock = 5;
            var transactionValueMock = 100;
            var accountBalanceMock = 100;
            var newAccountBalanceMock = accountBalanceMock - transactionValueMock;

            var accountMock = new Finance.Core.Entities.Account("Account Description 1", "Color 1", accountBalanceMock, 1);
            var categoryMock = new Finance.Core.Entities.Category("Category Description 1", "Color 1", Finance.Core.Enums.CategoryType.Income, 1);
            var transactionMock = new Finance.Core.Entities.Transaction("Description 1", DateTime.Now, transactionValueMock, 1, 1, accountMock, categoryMock);

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            transactionRepositoryMock.Setup(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock))).ReturnsAsync(transactionMock);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var deleteTransactionCommand = new DeleteTransactionCommand(transactionIdMock);
            var deleteTransactionCommandHandler = new DeleteTransactionCommandHandler(unitOfWorkMock.Object);

            // Act
            await deleteTransactionCommandHandler.Handle(deleteTransactionCommand, new CancellationToken());

            // Assert
            Assert.Equal(accountMock.Balance, newAccountBalanceMock);

            transactionRepositoryMock.Verify(tr => tr.Remove(It.IsAny<Finance.Core.Entities.Transaction>()), Times.Once);
            unitOfWorkMock.Verify(uw => uw.BeginTransactionAsync(), Times.Once);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.AtLeastOnce);
            unitOfWorkMock.Verify(uw => uw.CommitAsync(), Times.Once);
        }

        [Fact]
        public async void TransactionWithIdTwoNotExists_Executed_ThrowTransactionNotExistsException()
        {
            // Arrange
            var transactionIdMock = 2;

            Finance.Core.Entities.Transaction transactionMock = null;

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            transactionRepositoryMock.Setup(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock))).ReturnsAsync(transactionMock);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var deleteTransactionCommand = new DeleteTransactionCommand(transactionIdMock);
            var deleteTransactionCommandHandler = new DeleteTransactionCommandHandler(unitOfWorkMock.Object);

            // Act
            var act = () => deleteTransactionCommandHandler.Handle(deleteTransactionCommand, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<TransactionNotExistsException>(act);

            transactionRepositoryMock.Verify(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock)), Times.Once);
            transactionRepositoryMock.Verify(tr => tr.GetByIdAsync(It.Is<int>(id => id != transactionIdMock)), Times.Never);

            unitOfWorkMock.Verify(uw => uw.BeginTransactionAsync(), Times.Never);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.Never);
            unitOfWorkMock.Verify(uw => uw.CommitAsync(), Times.Never);
        }
    }
}
