namespace Shops.Exceptions
{
    public class SomethingNotFoundException : Exception
    {
        private SomethingNotFoundException(string message)
            : base(message) { }

        public static SomethingNotFoundException Product()
        {
            return new SomethingNotFoundException("The product didn't find");
        }

        public static SomethingNotFoundException Shop()
        {
            return new SomethingNotFoundException("The shop didn't find");
        }
    }
}
