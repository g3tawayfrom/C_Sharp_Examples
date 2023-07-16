namespace Isu.Extra.Exceptions
{
    public class NoMoreOptionsException : Exception
    {
        public NoMoreOptionsException()
            : base("Студент уже выбрал все курсы") { }
    }
}
