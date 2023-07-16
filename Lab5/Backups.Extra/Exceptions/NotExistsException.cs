namespace Backups.Extra.Exceptions
{
    public class NotExistsException : Exception
    {
        public NotExistsException()
            : base("Such element does not exist")
        { }
    }
}
