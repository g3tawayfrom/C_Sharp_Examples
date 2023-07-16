namespace Backups.Extra
{
    public class RestorePointProxy
    {
        public RestorePointProxy() { }

        public RestorePointProxy(List<Backups.BackupObject> backupObjectCollection)
        {
            BasicPoint = new Backups.RestorePoint(backupObjectCollection);

            BackupObjectCollection = new List<BackupObjectProxy>();
            foreach (Backups.BackupObject backupObject in backupObjectCollection)
            {
                BackupObjectCollection.Add(new BackupObjectProxy(backupObject.FilePath));
            }

            CreationTime = BasicPoint.CreationTime;
            BackupStorage = string.Empty;
        }

        public Backups.RestorePoint? BasicPoint { get; }
        public List<BackupObjectProxy> BackupObjectCollection { get; set; } = new List<BackupObjectProxy>();
        public DateTime CreationTime { get; set; }
        public string BackupStorage { get; set; } = string.Empty;
    }
}
