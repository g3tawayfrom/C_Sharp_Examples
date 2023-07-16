namespace Backups.Extra.Exceptions
{
    public class NoRestorePointsBeException : Exception
    {
        public NoRestorePointsBeException()
            : base("Can't delete all of restore points")
        { }
    }
}
