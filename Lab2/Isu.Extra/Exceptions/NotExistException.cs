namespace Isu.Extra.Exceptions
{
    public class NotExistException : Exception
    {
        public NotExistException()
            : base("Подобного элемента не существует в системе") { }
    }
}
