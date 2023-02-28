using Finance.Application.Queries.Transaction.GetAll;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Queries.Transaction
{
    public class GetAllTransactionsQueryHandlerTests
    {
        [Fact]
        public async void FourTransactionsExists_Executed_ReturnFourTransactionDetailsViewModels() // GIVEN_WHEN_THEN
        {
            // Arrange
            var categoryMock = new Finance.Core.Entities.Category("Description 1", "Color 1", Finance.Core.Enums.CategoryType.Expense, 1);
            var accountMock = new Finance.Core.Entities.Account("Description 1", "Color 1", 100, 1);

            var paginationTransactionsMock = new PaginationResult<Finance.Core.Entities.Transaction>
            {
                Data = new List<Finance.Core.Entities.Transaction>
                {
                    new Finance.Core.Entities.Transaction("Description 1", DateTime.Now, 100, 1, 1, accountMock, categoryMock),
                    new Finance.Core.Entities.Transaction("Description 2", DateTime.Now, 200, 2, 2, accountMock, categoryMock),
                    new Finance.Core.Entities.Transaction("Description 3", DateTime.Now, 300, 3, 3, accountMock, categoryMock),
                    new Finance.Core.Entities.Transaction("Description 4", DateTime.Now, 400, 4, 4, accountMock, categoryMock)
                }
            };

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            transactionRepositoryMock.Setup(tr => tr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(paginationTransactionsMock);
            unitOfWorkMock.SetupGet(uw => uw.Transaction).Returns(transactionRepositoryMock.Object);

            var getAllTransactionsQuery = new GetAllTransactionsQuery();
            var getAllTransactionsQueryHandler = new GetAllTransactionsQueryHandler(unitOfWorkMock.Object);

            // Act
            var paginationTransactionsDetails = await getAllTransactionsQueryHandler.Handle(getAllTransactionsQuery, new CancellationToken());

            // Assert
            Assert.NotNull(paginationTransactionsDetails);
            Assert.NotNull(paginationTransactionsDetails.Data);
            Assert.NotEmpty(paginationTransactionsDetails.Data);
            Assert.Equal(paginationTransactionsMock.Data.Count, paginationTransactionsDetails.Data.Count);

            transactionRepositoryMock.Verify(tr => tr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }
    }
}
