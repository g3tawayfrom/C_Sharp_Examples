namespace Shops.Exceptions
{
    public class ProductAlreadyExistsException : Exception
    {
        public ProductAlreadyExistsException()
            : base("Such product already exists in the shop")
        { }
    }
}
