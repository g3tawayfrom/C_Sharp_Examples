namespace Banks.Exceptions
{
    public class DoubleCancelationException : Exception
    {
        public DoubleCancelationException()
            : base("Transaction already cancelled") { }
    }
}
