namespace Backups.Extra
{
    public class BackupProxy
    {
        public BackupProxy()
        {
            RestorePoints = new List<RestorePointProxy>();
        }

        public List<RestorePointProxy> RestorePoints { get; set; }

        public void AddRestorePointToBackup(RestorePointProxy restorePoint)
        {
            RestorePoints.Add(restorePoint);
        }
    }
}
