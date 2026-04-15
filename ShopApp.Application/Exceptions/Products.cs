namespace ShopApp.Application.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int id)
            : base($"Product with such id {id} was not found")
        {
        }
    }
    public class ProductAlreadyExistsException : Exception
    {
        public ProductAlreadyExistsException(string name)
            : base($"Product with such name {name} already exists!")
        {
        }
    }
}