namespace Backups
{
    public class Backup
    {
        private List<RestorePoint> restorePoints;
        public Backup()
        {
            restorePoints = new List<RestorePoint>();
        }

        public IReadOnlyList<RestorePoint> RestorePoints => restorePoints;

        public void AddRestorePointToBackup(RestorePoint restorePoint)
        {
            restorePoints.Add(restorePoint);
        }
    }
}
