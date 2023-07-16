namespace Isu.Exceptions
{
    public class StudentNameException : Exception
    {
        public StudentNameException()
            : base("Некорректное имя студента")
        { }
    }
}
