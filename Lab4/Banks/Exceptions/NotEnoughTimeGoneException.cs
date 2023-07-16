namespace Banks.Exceptions
{
    public class NotEnoughTimeGoneException : Exception
    {
        public NotEnoughTimeGoneException()
            : base("You can not do it, still") { }
    }
}
