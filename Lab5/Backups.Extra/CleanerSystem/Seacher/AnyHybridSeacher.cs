namespace Backups.Extra.CleanerSystem
{
    public class AnyHybridSeacher : ISeacher
    {
        public void FindWastedRestorePoints(BackupServiceProxy backupService, List<ILimit> limits, List<RestorePointProxy> returnList)
        {
            foreach (ILimit limit in limits)
            {
                limit.LimitCheck(backupService.Backup.RestorePoints);

                foreach (RestorePointProxy wastedRestorePoint in limit.GetWastedRestorePoints())
                {
                    if (!returnList.Contains(wastedRestorePoint))
                    {
                        returnList.Add(wastedRestorePoint);
                    }
                }
            }
        }
    }
}
