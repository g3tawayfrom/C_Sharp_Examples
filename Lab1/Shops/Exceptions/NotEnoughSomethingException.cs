namespace Shops.Exceptions
{
    public class NotEnoughSomethingException : Exception
    {
        private NotEnoughSomethingException(string message)
            : base(message) { }

        public static NotEnoughSomethingException Amount()
        {
            return new NotEnoughSomethingException($"There's not enough amount of products here");
        }

        public static NotEnoughSomethingException Money()
        {
            return new NotEnoughSomethingException($"There's not enough money here");
        }
    }
}
