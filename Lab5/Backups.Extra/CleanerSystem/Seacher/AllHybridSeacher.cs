namespace Backups.Extra.CleanerSystem
{
    public class AllHybridSeacher : ISeacher
    {
        public void FindWastedRestorePoints(BackupServiceProxy backupService, List<ILimit> limits, List<RestorePointProxy> returnList)
        {
            foreach (ILimit limit in limits)
            {
                limit.LimitCheck(backupService.Backup.RestorePoints);
            }

            foreach (RestorePointProxy restorePoint in backupService.Backup.RestorePoints)
            {
                if (limits.All(l => l.GetWastedRestorePoints().Contains(restorePoint)))
                    returnList.Add(restorePoint);
            }
        }
    }
}
