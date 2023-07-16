namespace Shops.Exceptions
{
    public class IncorrectFormException : Exception
    {
        private IncorrectFormException(string message)
            : base(message) { }

        public static IncorrectFormException Address()
        {
            return new IncorrectFormException($"Incorrect address");
        }

        public static IncorrectFormException Name()
        {
            return new IncorrectFormException($"Incorrect name");
        }

        public static IncorrectFormException Price()
        {
            return new IncorrectFormException($"Incorrect price");
        }

        public static IncorrectFormException Sum()
        {
            return new IncorrectFormException($"Incorrect sum");
        }

        public static IncorrectFormException Amount()
        {
            return new IncorrectFormException($"Incorrect amount");
        }
    }
}
