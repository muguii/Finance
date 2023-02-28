using Finance.Core.Entities;

namespace Finance.UnitTests.Core.Entities
{
    public class AccountTests
    {
        [Fact]
        public void AccountWithIdFiveExists_Executed_IncreaseAccountBalance() //GIVEN_WHEN_THEN
        {
            // Arrange
            var accountBalance = 100;
            var accountCreditValue = 100;

            var accountMock = new Account("Description 1", "Color 1", accountBalance, 1);

            // Act
            accountMock.Credit(accountCreditValue);

            // Assert
            Assert.Equal(accountMock.Balance, accountBalance + accountCreditValue);
        }

        [Fact]
        public void AccountWithIdFiveExists_Executed_DecreaseAccountBalance() //GIVEN_WHEN_THEN
        {
            // Arrange
            var accountBalance = 100;
            var accountDebitValue = 100;

            var accountMock = new Account("Description 1", "Color 1", accountBalance, 1);

            // Act
            accountMock.Debit(accountDebitValue);

            // Assert
            Assert.Equal(accountMock.Balance, accountBalance - accountDebitValue);
        }
    }
}
