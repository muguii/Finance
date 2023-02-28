using Finance.Application.Queries.Category.GetAll;
using Finance.Core.Models;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Queries.Category
{
    public class GetAllCategoriesQueryHandlerTests
    {
        [Fact]
        public async void TwoCategoryExists_Executed_ReturnTwoCategoryViewModels() // GIVEN_WHEN_THEN
        {
            // Arrange
            var paginationCategoriesMock = new PaginationResult<Finance.Core.Entities.Category>
            {
                Data = new List<Finance.Core.Entities.Category>
                {
                    new Finance.Core.Entities.Category("Description 1", "Color 1", Finance.Core.Enums.CategoryType.Expense, 1),
                    new Finance.Core.Entities.Category("Description 2", "Color 2", Finance.Core.Enums.CategoryType.Income, 2)
                }
            };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock.Setup(cr => cr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(paginationCategoriesMock);
            unitOfWorkMock.SetupGet(uw => uw.Category).Returns(categoryRepositoryMock.Object);

            var getAllCategoriesQuery = new GetAllCategoriesQuery();
            var getAllCategoriesQueryHandler = new GetAllCategoriesQueryHandler(unitOfWorkMock.Object);

            // Act
            var paginationCategoriesViewModels = await getAllCategoriesQueryHandler.Handle(getAllCategoriesQuery, new CancellationToken());

            // Assert
            Assert.NotNull(paginationCategoriesViewModels);
            Assert.NotNull(paginationCategoriesViewModels.Data);
            Assert.NotEmpty(paginationCategoriesViewModels.Data);
            Assert.Equal(paginationCategoriesMock.Data.Count, paginationCategoriesViewModels.Data.Count);

            categoryRepositoryMock.Verify(cr => cr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }
    }
}
