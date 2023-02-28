using Finance.Application.Queries.Category.GetById;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Queries.Category
{
    public class GetCategoryByIdQueryHandlerTests
    {
        [Fact]
        public async void CategoryWithIdOneExists_Executed_ReturnCategoryDetailsViewModelOfCategoryOne() // GIVEN_WHEN_THEN
        {
            // Arrange
            var categoryIdMock = 1;
            var categoryMock = new Finance.Core.Entities.Category("Description 1", "Color 1", Finance.Core.Enums.CategoryType.Expense, 1);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(cr => cr.GetByIdAsync(It.Is<int>(id => id == categoryIdMock))).ReturnsAsync(categoryMock);
            unitOfWorkMock.SetupGet(uw => uw.Category).Returns(categoryRepositoryMock.Object);

            var getCategoryByIdQuery = new GetCategoryByIdQuery(categoryIdMock);
            var getCategoryByIdQueryHandler = new GetCategoryByIdQueryHandler(unitOfWorkMock.Object);

            // Act
            var categoryDetails = await getCategoryByIdQueryHandler.Handle(getCategoryByIdQuery, new CancellationToken());

            // Assert
            Assert.NotNull(categoryDetails);
            Assert.Equal(categoryMock.Description, categoryDetails.Description);
            Assert.Equal(categoryMock.Color, categoryDetails.Color);
            Assert.Equal(categoryMock.Type, categoryDetails.Type);

            categoryRepositoryMock.Verify(cr => cr.GetByIdAsync(It.Is<int>(id => id == categoryIdMock)), Times.Once);
            categoryRepositoryMock.Verify(cr => cr.GetByIdAsync(It.Is<int>(id => id != categoryIdMock)), Times.Never);
        }

        [Fact]
        public async void CategoryWithIdFiveNotExists_Executed_ReturnNull() // GIVEN_WHEN_THEN
        {
            // Arrange
            var categoryIdMock = 5;
            Finance.Core.Entities.Category categoryMock = null;

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(cr => cr.GetByIdAsync(It.Is<int>(id => id == categoryIdMock))).ReturnsAsync(categoryMock);
            unitOfWorkMock.SetupGet(uw => uw.Category).Returns(categoryRepositoryMock.Object);

            var getCategoryByIdQuery = new GetCategoryByIdQuery(categoryIdMock);
            var getCategoryByIdQueryHandler = new GetCategoryByIdQueryHandler(unitOfWorkMock.Object);

            // Act
            var categoryDetails = await getCategoryByIdQueryHandler.Handle(getCategoryByIdQuery, new CancellationToken());

            // Assert
            Assert.Null(categoryDetails);

            categoryRepositoryMock.Verify(cr => cr.GetByIdAsync(It.Is<int>(id => id == categoryIdMock)), Times.Once);
            categoryRepositoryMock.Verify(cr => cr.GetByIdAsync(It.Is<int>(id => id != categoryIdMock)), Times.Never);
        }
    }
}
