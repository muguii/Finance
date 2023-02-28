using Finance.Application.Queries.Transaction.GetById;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Queries.Transaction
{
    public class GetTransactionByIdQueryHandlerTests
    {
        [Fact]
        public async void TransactionWithIdTenExists_Executed_ReturnTransactionDetailsViewModelOfTransactionTen() // GIVEN_WHEN_THEN
        {
            // Arrange
            var transactionIdMock = 10;
            var categoryMock = new Finance.Core.Entities.Category("Description 1", "Color 1", Finance.Core.Enums.CategoryType.Expense, 1);
            var accountMock = new Finance.Core.Entities.Account("Description 1", "Color 1", 100, 1);

            var transactionMock = new Finance.Core.Entities.Transaction("Description 1", DateTime.Now, 100, 1, 1, accountMock, categoryMock);

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            transactionRepositoryMock.Setup(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock))).ReturnsAsync(transactionMock);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var getTransactionByIdQuery = new GetTransactionByIdQuery(transactionIdMock);
            var getTransactionByIdQueryHandler = new GetTransactionByIdQueryHandler(unitOfWorkMock.Object);

            // Act
            var transactionDetails = await getTransactionByIdQueryHandler.Handle(getTransactionByIdQuery, new CancellationToken());

            // Assert
            Assert.NotNull(transactionDetails);
            Assert.Equal(transactionMock.Description, transactionDetails.Description);
            Assert.Equal(transactionMock.Date, transactionDetails.Date);
            Assert.Equal(transactionMock.Value, transactionDetails.Value);

            transactionRepositoryMock.Verify(cr => cr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock)), Times.Once);
            transactionRepositoryMock.Verify(cr => cr.GetByIdAsync(It.Is<int>(id => id != transactionIdMock)), Times.Never);
        }

        [Fact]
        public async void TransactionWithIdFiveNotExists_Executed_ReturnNull() // GIVEN_WHEN_THEN
        {
            // Arrange
            var transactionIdMock = 10;
            Finance.Core.Entities.Transaction transactionMock = null;

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            transactionRepositoryMock.Setup(tr => tr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock))).ReturnsAsync(transactionMock);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var getTransactionByIdQuery = new GetTransactionByIdQuery(transactionIdMock);
            var getTransactionByIdQueryHandler = new GetTransactionByIdQueryHandler(unitOfWorkMock.Object);

            // Act
            var transactionDetails = await getTransactionByIdQueryHandler.Handle(getTransactionByIdQuery, new CancellationToken());

            // Assert
            Assert.Null(transactionDetails);

            transactionRepositoryMock.Verify(cr => cr.GetByIdAsync(It.Is<int>(id => id == transactionIdMock)), Times.Once);
            transactionRepositoryMock.Verify(cr => cr.GetByIdAsync(It.Is<int>(id => id != transactionIdMock)), Times.Never);
        }
    }
}
