namespace Isu.Exceptions
{
    public class TakenIDException : Exception
    {
        public TakenIDException()
            : base("Данное ID уже занято")
        { }
    }
}
