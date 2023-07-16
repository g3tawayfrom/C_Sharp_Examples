namespace Banks.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException()
            : base("Such object wasn't found") { }
    }
}
