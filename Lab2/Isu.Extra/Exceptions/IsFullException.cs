namespace Isu.Extra.Exceptions
{
    public class IsFullException : Exception
    {
        public IsFullException()
            : base("Недостаточно мест") { }
    }
}
