using Finance.Application.Commands.Transaction.Create;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Commands.Transaction
{
    public class CreateTransactionCommandHandlerTests1
    {
        [Fact]
        public async void InputDataIsOkAndTransactionTypeIsExpense_Executed_DecreaseAccountBalanceAndReturnTransactionId() // GIVEN_WHEN_THEN
        {
            // Arrange
            var descriptionMock = "Transaction Description 1";
            var dateMock = DateTime.Now;
            var transactionValueMock = 100;
            var accountIdMock = 1;
            var categoryIdMock = 1;
            var accountBalanceMock = 100;

            var accountMock = new Finance.Core.Entities.Account("Account Description 1", "Color 1", accountBalanceMock, 1);
            var categoryMock = new Finance.Core.Entities.Category("Category Description 1", "Color 1", Finance.Core.Enums.CategoryType.Expense, 1);
            var transactionMock = new Finance.Core.Entities.Transaction(descriptionMock, dateMock, transactionValueMock, accountIdMock, categoryIdMock, accountMock, categoryMock);

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            transactionRepositoryMock.Setup(tr => tr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(transactionMock);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var createTransactionCommand = new CreateTransactionCommand()
            {
                Description = descriptionMock,
                Date = dateMock,
                Value = transactionValueMock,
                AccountId = accountIdMock,
                CategoryId = categoryIdMock
            };
            var createTransactionCommandHandler = new CreateTransactionCommandHandler(unitOfWorkMock.Object);

            // Act
            var transactionId = await createTransactionCommandHandler.Handle(createTransactionCommand, new CancellationToken());

            // Assert
            Assert.True(transactionId >= 0);
            Assert.Equal(accountMock.Balance, transactionValueMock - accountBalanceMock);

            transactionRepositoryMock.Verify(tr => tr.AddAsync(It.IsAny<Finance.Core.Entities.Transaction>()), Times.Once);
            transactionRepositoryMock.Verify(tr => tr.GetByIdAsync(It.IsAny<int>()), Times.Once);
            unitOfWorkMock.Verify(uw => uw.BeginTransactionAsync(), Times.Once);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.AtLeastOnce);
            unitOfWorkMock.Verify(uw => uw.CommitAsync(), Times.Once);
        }

        [Fact]
        public async void InputDataIsOkAndTransactionTypeIsIncome_Executed_IncreaseAccountBalanceAndReturnTransactionId() // GIVEN_WHEN_THEN
        {
            // Arrange
            var descriptionMock = "Transaction Description 1";
            var dateMock = DateTime.Now;
            var transactionValueMock = 100;
            var accountIdMock = 1;
            var categoryIdMock = 1;
            var accountBalanceMock = 100;

            var accountMock = new Finance.Core.Entities.Account("Account Description 1", "Color 1", accountBalanceMock, 1);
            var categoryMock = new Finance.Core.Entities.Category("Category Description 1", "Color 1", Finance.Core.Enums.CategoryType.Income, 1);
            var transactionMock = new Finance.Core.Entities.Transaction(descriptionMock, dateMock, transactionValueMock, accountIdMock, categoryIdMock, accountMock, categoryMock);

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            transactionRepositoryMock.Setup(tr => tr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(transactionMock);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var createTransactionCommand = new CreateTransactionCommand()
            {
                Description = descriptionMock,
                Date = dateMock,
                Value = transactionValueMock,
                AccountId = accountIdMock,
                CategoryId = categoryIdMock
            };
            var createTransactionCommandHandler = new CreateTransactionCommandHandler(unitOfWorkMock.Object);

            // Act
            var transactionId = await createTransactionCommandHandler.Handle(createTransactionCommand, new CancellationToken());

            // Assert
            Assert.True(transactionId >= 0);
            Assert.Equal(accountMock.Balance, transactionValueMock + accountBalanceMock);

            transactionRepositoryMock.Verify(tr => tr.AddAsync(It.IsAny<Finance.Core.Entities.Transaction>()), Times.Once);
            transactionRepositoryMock.Verify(tr => tr.GetByIdAsync(It.IsAny<int>()), Times.Once);
            unitOfWorkMock.Verify(uw => uw.BeginTransactionAsync(), Times.Once);
            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.AtLeastOnce);
            unitOfWorkMock.Verify(uw => uw.CommitAsync(), Times.Once);
        }
    }
}
