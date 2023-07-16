namespace Isu.Extra.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException()
            : base("Подобной экземляр уже существует") { }
    }
}
