namespace Isu.Exceptions
{
    public class TakenGroupNameException : Exception
    {
        public TakenGroupNameException()
            : base("Подобная группа уже существует")
        { }
    }
}
