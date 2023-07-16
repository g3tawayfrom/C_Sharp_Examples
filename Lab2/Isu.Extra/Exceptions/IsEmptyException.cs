namespace Isu.Extra.Exceptions
{
    public class IsEmptyException : Exception
    {
        public IsEmptyException()
            : base("Коллекция пуста") { }
    }
}
