namespace Isu.Exceptions
{
    public class GroupNameFormatException : Exception
    {
        public GroupNameFormatException()
            : base("Некорректное имя группы")
        { }
    }
}
