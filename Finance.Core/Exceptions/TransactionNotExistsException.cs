namespace Finance.Core.Exceptions
{
    public class TransactionNotExistsException : Exception
    {
        public TransactionNotExistsException(int transactionId) : base($"There is no transaction with id {transactionId}.")
        {

        }
    }
}
