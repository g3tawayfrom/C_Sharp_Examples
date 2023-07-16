namespace Shops.Exceptions
{
    public class NoSuchProductSomewhereException : Exception
    {
        private NoSuchProductSomewhereException(string message)
            : base(message) { }

        public static NoSuchProductSomewhereException InGeneral()
        {
            return new NoSuchProductSomewhereException("There's no product anywhere");
        }

        public static NoSuchProductSomewhereException InDirectShop()
        {
            return new NoSuchProductSomewhereException("There's no product in this shop");
        }
    }
}
