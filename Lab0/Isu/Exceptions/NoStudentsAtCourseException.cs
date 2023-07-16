namespace Isu.Exceptions
{
    public class NoStudentsAtCourseException : Exception
    {
        public NoStudentsAtCourseException()
            : base("На данном курсе нет студентов")
        { }
    }
}
