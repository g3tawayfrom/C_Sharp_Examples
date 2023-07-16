namespace Banks.Exceptions
{
    public class WrongSumException : Exception
    {
        public WrongSumException()
            : base("Incorrect sum") { }
    }
}
