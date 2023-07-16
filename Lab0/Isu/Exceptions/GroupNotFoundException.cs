namespace Isu.Exceptions
{
    public class GroupNotFoundException : Exception
    {
        public GroupNotFoundException()
            : base("Данной группы нет в системе")
        { }
    }
}
