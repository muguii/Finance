namespace Finance.Core.Exceptions
{
    public class UserNotExistsException : Exception
    {
        public UserNotExistsException(int userId) : base($"There is no user with id {userId}.")
        {

        }
    }
}
