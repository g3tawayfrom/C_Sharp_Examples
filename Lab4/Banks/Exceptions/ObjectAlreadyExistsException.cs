namespace Banks.Exceptions
{
    public class ObjectAlreadyExistsException : Exception
    {
        public ObjectAlreadyExistsException()
            : base("Such object already exists") { }
    }
}
