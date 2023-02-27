namespace Finance.Core.Exceptions
{
    public class CategoryNotExistsException : Exception
    {
        public CategoryNotExistsException(int categoryId) : base($"There is no category with id {categoryId}.")
        {

        }
    }
}
