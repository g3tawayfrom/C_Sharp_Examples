namespace Isu.Exceptions
{
    public class NoGroupsAtCourseException : Exception
    {
        public NoGroupsAtCourseException()
            : base("На данном курсе нет учебных групп")
        { }
    }
}
