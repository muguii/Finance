using Finance.Application.Queries.Account.GetAll;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Queries.Account
{
    public class GetAllAccountsQueryHandlerTests
    {
        [Fact]
        public async void TwoAccountsExists_Executed_ReturnTwoAccountViewModels() // GIVEN_WHEN_THEN
        {
            // Arrange
            var paginationAccountsMock = new PaginationResult<Finance.Core.Entities.Account>
            {
                Data = new List<Finance.Core.Entities.Account>
                {
                    new Finance.Core.Entities.Account("Description 1", "Color 1", 100, 1),
                    new Finance.Core.Entities.Account("Description 2", "Color 2", 200, 2)
                }
            };

            var accountRepositoryMock = new Mock<IAccountRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            accountRepositoryMock.Setup(ar => ar.GetAllAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(paginationAccountsMock);
            unitOfWorkMock.SetupGet(uw => uw.Account).Returns(accountRepositoryMock.Object);

            var getAllAccountsQuery = new GetAllAccountsQuery();
            var getAllAccountsQueryHandler = new GetAllAccountsQueryHandler(unitOfWorkMock.Object);

            // Act
            var paginationAccountViewModels = await getAllAccountsQueryHandler.Handle(getAllAccountsQuery, new CancellationToken());

            // Assert
            Assert.NotNull(paginationAccountViewModels);
            Assert.NotNull(paginationAccountViewModels.Data);
            Assert.NotEmpty(paginationAccountViewModels.Data);
            Assert.Equal(paginationAccountsMock.Data.Count, paginationAccountViewModels.Data.Count);

            accountRepositoryMock.Verify(ar => ar.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }
    }
}
