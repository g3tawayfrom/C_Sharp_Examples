namespace Backups
{
    public class RestorePoint
    {
        public RestorePoint(List<BackupObject> backupObjectCollection)
        {
            BackupObjectCollection = backupObjectCollection;
            CreationTime = DateTime.Now;
        }

        public List<BackupObject> BackupObjectCollection { get; }

        public DateTime CreationTime { get; }
    }
}
