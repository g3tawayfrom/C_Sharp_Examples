namespace Isu.Exceptions
{
    public class StudentNotFoundException : Exception
    {
        public StudentNotFoundException()
            : base("Данного студента нет в системе")
        { }
    }
}
