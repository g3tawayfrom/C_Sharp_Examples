namespace Isu.Exceptions
{
    public class CourseNumberException : Exception
    {
        public CourseNumberException()
            : base("Некорректный номер курса")
        { }
    }
}
