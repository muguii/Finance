namespace Finance.Core.Exceptions
{
    public class AccountNotExistsException : Exception
    {
        public AccountNotExistsException(int accountId) : base($"There is no account with id {accountId}.")
        {
            
        }
    }
}
