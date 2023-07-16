namespace Backups.Extra.RestoreSystem
{
    public interface IRestoreSystem
    {
        void Restore(RestorePointProxy restorePoint, string? newPath = null);
    }
}
