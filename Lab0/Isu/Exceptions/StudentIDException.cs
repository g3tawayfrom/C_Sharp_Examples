namespace Isu.Exceptions
{
    public class StudentIDException : Exception
    {
        public StudentIDException()
            : base("Некорректный ID")
        { }
    }
}
