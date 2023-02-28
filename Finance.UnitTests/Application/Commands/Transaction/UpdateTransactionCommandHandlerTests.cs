using Finance.Application.Commands.Transaction.Update;
using Finance.Core.Exceptions;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Commands.Transaction
{
    public class UpdateTransactionCommandHandlerTests
    {
        [Fact]
        public async void TransactionWithIdFiveExistsAndTransactionValueNotChanged_Executed_ChangeTransactionData() // GIVEN_WHEN_THEN
        {
            // Arrange
            var transactionIdMock = 5;
            var descriptionMock = "Description 1";
            var dateMock = DateTime.Now;
            var transactionValueMock = 100;
            var accountIdMock = 1;
            var categoryIdMock = 1;

            var transactionMock = new Finance.Core.Entities.Transaction(descriptionMock, dateMock, transactionValueMock, accountIdMock, categoryIdMock);

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            transactionRepositoryMock.Setup(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock))).ReturnsAsync(transactionMock);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var updateTransactionCommand = new UpdateTransactionCommand()
            {
                Description = "New Description",
                Date = DateTime.Now.AddSeconds(10),
                Value = transactionValueMock,
                TransactionId = transactionIdMock
            };
            var updateTransactionCommandHandler = new UpdateTransactionCommandHandler(unitOfWorkMock.Object);

            Assert.Equal(transactionMock.LastUpdate, transactionMock.CreatedAt);

            // Act
            await updateTransactionCommandHandler.Handle(updateTransactionCommand, new CancellationToken());

            // Assert
            Assert.NotEqual(descriptionMock, transactionMock.Description);
            Assert.NotEqual(dateMock, transactionMock.Date);
            Assert.NotEqual(transactionMock.LastUpdate, transactionMock.CreatedAt);
            Assert.Equal(transactionValueMock, transactionMock.Value);
            Assert.Equal(accountIdMock, transactionMock.AccountId);
            Assert.Equal(categoryIdMock, transactionMock.CategoryId);

            transactionRepositoryMock.Verify(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock)), Times.Once);
            transactionRepositoryMock.Verify(tr => tr.GetByIdAsync(It.Is<int>(id => id != transactionIdMock)), Times.Never);

            unitOfWorkMock.Verify(uw => uw.BeginTransactionAsync(), Times.Once);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.AtLeastOnce);
            unitOfWorkMock.Verify(uw => uw.CommitAsync(), Times.Once);
        }

        [Fact] 
        public async void TransactionWithIdFiveExistsAndTransactionTypeIsExpenseAndTransactionValueIncreased_Executed_DecreaseAccountBalanceAndChangeTransactionData() // GIVEN_WHEN_THEN
        {
            // Arrange
            var transactionIdMock = 5;
            var descriptionMock = "Description 1";
            var dateMock = DateTime.Now;
            var transactionValueMock = 100;
            var accountIdMock = 1;
            var categoryIdMock = 1; 
            var accountBalanceMock = 200;
            var newTransactionValueMock = 150;
            var adjustmentValueMock = newTransactionValueMock - transactionValueMock;

            var accountMock = new Finance.Core.Entities.Account("Account Description 1", "Color 1", accountBalanceMock, 1);
            var categoryMock = new Finance.Core.Entities.Category("Category Description 1", "Color 1", Finance.Core.Enums.CategoryType.Expense, 1);
            var transactionMock = new Finance.Core.Entities.Transaction(descriptionMock, dateMock, transactionValueMock, accountIdMock, categoryIdMock, accountMock, categoryMock);

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            transactionRepositoryMock.Setup(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock))).ReturnsAsync(transactionMock);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var updateTransactionCommand = new UpdateTransactionCommand()
            {
                Description = "New Description",
                Date = DateTime.Now.AddSeconds(10),
                Value = newTransactionValueMock,
                TransactionId = transactionIdMock
            };
            var updateTransactionCommandHandler = new UpdateTransactionCommandHandler(unitOfWorkMock.Object);

            Assert.Equal(transactionMock.LastUpdate, transactionMock.CreatedAt);

            // Act
            await updateTransactionCommandHandler.Handle(updateTransactionCommand, new CancellationToken());

            // Assert
            Assert.Equal(accountMock.Balance, accountBalanceMock - adjustmentValueMock);
            Assert.NotEqual(transactionValueMock, transactionMock.Value);
            Assert.NotEqual(descriptionMock, transactionMock.Description);
            Assert.NotEqual(dateMock, transactionMock.Date);
            Assert.NotEqual(transactionMock.LastUpdate, transactionMock.CreatedAt);
            Assert.Equal(accountIdMock, transactionMock.AccountId);
            Assert.Equal(categoryIdMock, transactionMock.CategoryId);

            transactionRepositoryMock.Verify(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock)), Times.Once);
            transactionRepositoryMock.Verify(tr => tr.GetByIdAsync(It.Is<int>(id => id != transactionIdMock)), Times.Never);

            unitOfWorkMock.Verify(uw => uw.BeginTransactionAsync(), Times.Once);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.AtLeastOnce);
            unitOfWorkMock.Verify(uw => uw.CommitAsync(), Times.Once);
        }

        [Fact] 
        public async void TransactionWithIdFiveExistsAndTransactionTypeIsExpenseAndTransactionValueDecreased_Executed_IncreaseAccountBalanceAndChangeTransactionData() // GIVEN_WHEN_THEN
        {
            // Arrange
            const int positiveValue = -1;

            var transactionIdMock = 5;
            var descriptionMock = "Description 1";
            var dateMock = DateTime.Now;
            var transactionValueMock = 100;
            var accountIdMock = 1;
            var categoryIdMock = 1;
            var accountBalanceMock = 200;
            var newTransactionValueMock = 50;
            var adjustmentValueMock = (newTransactionValueMock - transactionValueMock) * positiveValue;

            var accountMock = new Finance.Core.Entities.Account("Account Description 1", "Color 1", accountBalanceMock, 1);
            var categoryMock = new Finance.Core.Entities.Category("Category Description 1", "Color 1", Finance.Core.Enums.CategoryType.Expense, 1);
            var transactionMock = new Finance.Core.Entities.Transaction(descriptionMock, dateMock, transactionValueMock, accountIdMock, categoryIdMock, accountMock, categoryMock);

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            transactionRepositoryMock.Setup(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock))).ReturnsAsync(transactionMock);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var updateTransactionCommand = new UpdateTransactionCommand()
            {
                Description = "New Description",
                Date = DateTime.Now.AddSeconds(10),
                Value = newTransactionValueMock,
                TransactionId = transactionIdMock
            };
            var updateTransactionCommandHandler = new UpdateTransactionCommandHandler(unitOfWorkMock.Object);

            Assert.Equal(transactionMock.LastUpdate, transactionMock.CreatedAt);

            // Act
            await updateTransactionCommandHandler.Handle(updateTransactionCommand, new CancellationToken());

            // Assert
            Assert.Equal(accountMock.Balance, accountBalanceMock + adjustmentValueMock);
            Assert.NotEqual(transactionValueMock, transactionMock.Value);
            Assert.NotEqual(descriptionMock, transactionMock.Description);
            Assert.NotEqual(dateMock, transactionMock.Date);
            Assert.NotEqual(transactionMock.LastUpdate, transactionMock.CreatedAt);
            Assert.Equal(accountIdMock, transactionMock.AccountId);
            Assert.Equal(categoryIdMock, transactionMock.CategoryId);

            transactionRepositoryMock.Verify(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock)), Times.Once);
            transactionRepositoryMock.Verify(tr => tr.GetByIdAsync(It.Is<int>(id => id != transactionIdMock)), Times.Never);

            unitOfWorkMock.Verify(uw => uw.BeginTransactionAsync(), Times.Once);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.AtLeastOnce);
            unitOfWorkMock.Verify(uw => uw.CommitAsync(), Times.Once);
        }

        [Fact]
        public async void TransactionWithIdFiveExistsAndTransactionTypeIsIncomeAndTransactionValueIncreased_Executed_IncreaseAccountBalanceAndChangeTransactionData() // GIVEN_WHEN_THEN
        {
            // Arrange
            var transactionIdMock = 5;
            var descriptionMock = "Description 1";
            var dateMock = DateTime.Now;
            var transactionValueMock = 100;
            var accountIdMock = 1;
            var categoryIdMock = 1;
            var accountBalanceMock = 200;
            var newTransactionValueMock = 150;
            var adjustmentValueMock = newTransactionValueMock - transactionValueMock;

            var accountMock = new Finance.Core.Entities.Account("Account Description 1", "Color 1", accountBalanceMock, 1);
            var categoryMock = new Finance.Core.Entities.Category("Category Description 1", "Color 1", Finance.Core.Enums.CategoryType.Income, 1);
            var transactionMock = new Finance.Core.Entities.Transaction(descriptionMock, dateMock, transactionValueMock, accountIdMock, categoryIdMock, accountMock, categoryMock);

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            transactionRepositoryMock.Setup(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock))).ReturnsAsync(transactionMock);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var updateTransactionCommand = new UpdateTransactionCommand()
            {
                Description = "New Description",
                Date = DateTime.Now.AddSeconds(10),
                Value = newTransactionValueMock,
                TransactionId = transactionIdMock
            };
            var updateTransactionCommandHandler = new UpdateTransactionCommandHandler(unitOfWorkMock.Object);

            Assert.Equal(transactionMock.LastUpdate, transactionMock.CreatedAt);

            // Act
            await updateTransactionCommandHandler.Handle(updateTransactionCommand, new CancellationToken());

            // Assert
            Assert.Equal(accountMock.Balance, accountBalanceMock + adjustmentValueMock);
            Assert.NotEqual(transactionValueMock, transactionMock.Value);
            Assert.NotEqual(descriptionMock, transactionMock.Description);
            Assert.NotEqual(dateMock, transactionMock.Date);
            Assert.NotEqual(transactionMock.LastUpdate, transactionMock.CreatedAt);
            Assert.Equal(accountIdMock, transactionMock.AccountId);
            Assert.Equal(categoryIdMock, transactionMock.CategoryId);

            transactionRepositoryMock.Verify(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock)), Times.Once);
            transactionRepositoryMock.Verify(tr => tr.GetByIdAsync(It.Is<int>(id => id != transactionIdMock)), Times.Never);

            unitOfWorkMock.Verify(uw => uw.BeginTransactionAsync(), Times.Once);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.AtLeastOnce);
            unitOfWorkMock.Verify(uw => uw.CommitAsync(), Times.Once);
        }

        [Fact]
        public async void TransactionWithIdFiveExistsAndTransactionTypeIsIncomeAndTransactionValueDecreased_Executed_DecreaseAccountBalanceAndChangeTransactionData() // GIVEN_WHEN_THEN
        {
            // Arrange
            const int positiveValue = -1;

            var transactionIdMock = 5;
            var descriptionMock = "Description 1";
            var dateMock = DateTime.Now;
            var transactionValueMock = 100;
            var accountIdMock = 1;
            var categoryIdMock = 1;
            var accountBalanceMock = 200;
            var newTransactionValueMock = 50;
            var adjustmentValueMock = (newTransactionValueMock - transactionValueMock) * positiveValue;

            var accountMock = new Finance.Core.Entities.Account("Account Description 1", "Color 1", accountBalanceMock, 1);
            var categoryMock = new Finance.Core.Entities.Category("Category Description 1", "Color 1", Finance.Core.Enums.CategoryType.Income, 1);
            var transactionMock = new Finance.Core.Entities.Transaction(descriptionMock, dateMock, transactionValueMock, accountIdMock, categoryIdMock, accountMock, categoryMock);

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            transactionRepositoryMock.Setup(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock))).ReturnsAsync(transactionMock);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var updateTransactionCommand = new UpdateTransactionCommand()
            {
                Description = "New Description",
                Date = DateTime.Now.AddSeconds(10),
                Value = newTransactionValueMock,
                TransactionId = transactionIdMock
            };
            var updateTransactionCommandHandler = new UpdateTransactionCommandHandler(unitOfWorkMock.Object);

            Assert.Equal(transactionMock.LastUpdate, transactionMock.CreatedAt);

            // Act
            await updateTransactionCommandHandler.Handle(updateTransactionCommand, new CancellationToken());

            // Assert
            Assert.Equal(accountMock.Balance, accountBalanceMock - adjustmentValueMock);
            Assert.NotEqual(transactionValueMock, transactionMock.Value);
            Assert.NotEqual(descriptionMock, transactionMock.Description);
            Assert.NotEqual(dateMock, transactionMock.Date);
            Assert.NotEqual(transactionMock.LastUpdate, transactionMock.CreatedAt);
            Assert.Equal(accountIdMock, transactionMock.AccountId);
            Assert.Equal(categoryIdMock, transactionMock.CategoryId);

            transactionRepositoryMock.Verify(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock)), Times.Once);
            transactionRepositoryMock.Verify(tr => tr.GetByIdAsync(It.Is<int>(id => id != transactionIdMock)), Times.Never);

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

            var updateTransactionCommand = new UpdateTransactionCommand()
            {
                Description = "New Description",
                Date = DateTime.Now.AddSeconds(10),
                Value = 150,
                TransactionId = transactionIdMock
            };
            var updateTransactionCommandHandler = new UpdateTransactionCommandHandler(unitOfWorkMock.Object);

            // Act
            var act = () => updateTransactionCommandHandler.Handle(updateTransactionCommand, new CancellationToken());

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
