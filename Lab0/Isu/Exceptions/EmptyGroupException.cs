namespace Isu.Exceptions
{
    public class EmptyGroupException : Exception
    {
        public EmptyGroupException()
            : base("Данная группа пуста")
        { }
    }
}
