using Finance.Application.Queries.Account.GetById;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Queries.Account
{
    public class GetAccountByIdQueryHandlerTests
    {
        [Fact]
        public async void AccountWithIdTwoExists_Executed_ReturnAccountDetailsViewModelOfAccountTwo() // GIVEN_WHEN_THEN
        {
            // Arrange
            var accountIdMock = 2;
            var accountMock = new Finance.Core.Entities.Account("Description 1", "Color 1", 100, 1);

            var accountRepositoryMock = new Mock<IAccountRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            accountRepositoryMock.Setup(ar => ar.GetByIdWithDetailsAsync(It.Is<int>(id => id == accountIdMock))).ReturnsAsync(accountMock);
            unitOfWorkMock.SetupGet(uw => uw.Account).Returns(accountRepositoryMock.Object);

            var getAccountByIdQuery = new GetAccountByIdQuery(accountIdMock);
            var getAccountByIdQueryHandler = new GetAccountByIdQueryHandler(unitOfWorkMock.Object);

            // Act
            var accountDetails = await getAccountByIdQueryHandler.Handle(getAccountByIdQuery, new CancellationToken());

            // Assert
            Assert.NotNull(accountDetails);
            Assert.Equal(accountMock.Description, accountDetails.Description);
            Assert.Equal(accountMock.Color, accountDetails.Color);
            Assert.Equal(accountMock.Balance, accountDetails.Balance);
            Assert.Equal(accountMock.InitialBalance, accountDetails.InitialBalance);

            accountRepositoryMock.Verify(ar => ar.GetByIdWithDetailsAsync(It.Is<int>(id => id == accountIdMock)), Times.Once);
            accountRepositoryMock.Verify(ar => ar.GetByIdWithDetailsAsync(It.Is<int>(id => id != accountIdMock)), Times.Never);
        }

        [Fact]
        public async void AccountWithIdFiveNotExists_Executed_ReturnNull() // GIVEN_WHEN_THEN
        {
            // Arrange
            var accountIdMock = 5;
            Finance.Core.Entities.Account accountMock = null;

            var accountRepositoryMock = new Mock<IAccountRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            accountRepositoryMock.Setup(ar => ar.GetByIdWithDetailsAsync(It.Is<int>(id => id == accountIdMock))).ReturnsAsync(accountMock);
            unitOfWorkMock.SetupGet(uw => uw.Account).Returns(accountRepositoryMock.Object);

            var getAccountByIdQuery = new GetAccountByIdQuery(accountIdMock);
            var getAccountByIdQueryHandler = new GetAccountByIdQueryHandler(unitOfWorkMock.Object);

            // Act
            var accountDetails = await getAccountByIdQueryHandler.Handle(getAccountByIdQuery, new CancellationToken());

            // Assert
            Assert.Null(accountDetails);

            accountRepositoryMock.Verify(ar => ar.GetByIdWithDetailsAsync(It.Is<int>(id => id == accountIdMock)), Times.Once);
            accountRepositoryMock.Verify(ar => ar.GetByIdWithDetailsAsync(It.Is<int>(id => id != accountIdMock)), Times.Never);
        }
    }
}
