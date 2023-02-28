using Finance.Application.Commands.User.Update;
using Finance.Core.Exceptions;
using Finance.Core.Repositories;
using Finance.Infrastructure.Persistence;
using Moq;

namespace Finance.UnitTests.Application.Commands.User
{
    public class UpdateUserCommandHandlerTests
    {
        [Fact]
        public async void UserWithIdFiveExistsAndInputDataIsOk_Executed_ChangeUserData() // GIVEN_WHEN_THEN
        {
            // Arrange
            var userIdMock = 5;
            var nameMock = "Name 1";
            var lastNameMock = "Last Name 1";
            var telephoneMock = "Telephone 1";
            var streetMock = "Street 1";
            var numberMock = "Number 1";
            var postalCodeMock = "Postal Code 1";
            var districtMock = "Distric 1";
            var cityMock = "City 1";
            var stateMock = "State 1";
            var countryMock = "Country 1";

            var addressMock = new Finance.Core.Entities.Address(userIdMock, streetMock, numberMock, postalCodeMock, districtMock, cityMock, stateMock, countryMock);
            var userMock = new Finance.Core.Entities.User("Login 1", "Password 1", "Email 1", nameMock, lastNameMock, DateTime.Now, "Gender 1", telephoneMock, "Role 1", addressMock);

            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            userRepositoryMock.Setup(ur => ur.GetByIdAsync(It.Is<int>(id => id == userIdMock))).ReturnsAsync(userMock);
            unitOfWorkMock.SetupGet(uw => uw.User).Returns(userRepositoryMock.Object);

            var updateUserCommand = new UpdateUserCommand()
            {
                UserId = userIdMock,
                Name = "New Name",
                LastName = "New Last Name",
                Telephone = "New Telephone",
                Street = "New Street",
                Number = "New Number",
                PostalCode = "New Postal Code",
                District = "New District",
                City = "New City",
                State = "New State",
                Country = "New Country"
            };
            var updateUserCommandHandler = new UpdateUserCommandHandler(unitOfWorkMock.Object);

            Assert.Equal(userMock.CreatedAt, userMock.LastUpdate);
            Assert.Equal(addressMock.CreatedAt, addressMock.LastUpdate);

            // Act
            await updateUserCommandHandler.Handle(updateUserCommand, new CancellationToken());

            // Assert
            Assert.NotEqual(nameMock, userMock.Name);
            Assert.NotEqual(lastNameMock, userMock.LastName);
            Assert.NotEqual(telephoneMock, userMock.Telephone);

            Assert.Equal(userIdMock, userMock.Address.UserId);
            Assert.NotEqual(streetMock, userMock.Address.Street);
            Assert.NotEqual(numberMock, userMock.Address.Number);
            Assert.NotEqual(postalCodeMock, userMock.Address.PostalCode);
            Assert.NotEqual(districtMock, userMock.Address.District);
            Assert.NotEqual(cityMock, userMock.Address.City);
            Assert.NotEqual(stateMock, userMock.Address.State);
            Assert.NotEqual(countryMock, userMock.Address.Country);

            Assert.NotEqual(userMock.CreatedAt, userMock.LastUpdate);
            Assert.NotEqual(addressMock.CreatedAt, addressMock.LastUpdate);

            userRepositoryMock.Verify(ur => ur.GetByIdAsync(It.Is<int>(id => id == userIdMock)), Times.Once);
            userRepositoryMock.Verify(ur => ur.GetByIdAsync(It.Is<int>(id => id != userIdMock)), Times.Never);

            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.AtLeastOnce);
        }

        [Fact]
        public async void AccountWithIdTwoNotExistsAndInputDataIsOk_Executed_ThrowAccountNotExistsException() // GIVEN_WHEN_THEN
        {
            // Arrange
            var userIdMock = 5;

            Finance.Core.Entities.User userMock = null;

            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            userRepositoryMock.Setup(ur => ur.GetByIdAsync(It.Is<int>(id => id == userIdMock))).ReturnsAsync(userMock);
            unitOfWorkMock.SetupGet(uw => uw.User).Returns(userRepositoryMock.Object);

            var updateUserCommand = new UpdateUserCommand()
            {
                UserId = userIdMock,
                Name = "New Name",
                LastName = "New Last Name",
                Telephone = "New Telephone",
                Street = "New Street",
                Number = "New Number",
                PostalCode = "New Postal Code",
                District = "New District",
                City = "New City",
                State = "New State",
                Country = "New Country"
            };
            var updateUserCommandHandler = new UpdateUserCommandHandler(unitOfWorkMock.Object);

            // Act
            var act = () => updateUserCommandHandler.Handle(updateUserCommand, new CancellationToken());

            // Assert
            await Assert.ThrowsAsync<UserNotExistsException>(act);

            userRepositoryMock.Verify(ur => ur.GetByIdAsync(It.Is<int>(id => id == userIdMock)), Times.Once);
            userRepositoryMock.Verify(ur => ur.GetByIdAsync(It.Is<int>(id => id != userIdMock)), Times.Never);

            unitOfWorkMock.Verify(uw => uw.CompleteAsync(), Times.Never);
        }
    }
}
