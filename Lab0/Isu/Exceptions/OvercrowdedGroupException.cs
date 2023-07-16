namespace Isu.Exceptions
{
    public class OvercrowdedGroupException : Exception
    {
        public OvercrowdedGroupException()
            : base("В группе уже максимум человек")
        { }
    }
}
