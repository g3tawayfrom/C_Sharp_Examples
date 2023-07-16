using Backups.Extra.Exceptions;

namespace Backups.Extra.CleanerSystem
{
    public class Merger : IExterminator
    {
        public void ExterminateWastedRestorePoints(BackupServiceProxy backupService, List<RestorePointProxy> wastedRestorePoints)
        {
            if (backupService.Backup != null && backupService.Backup.RestorePoints.Count == wastedRestorePoints.Count)
            {
                throw new NoRestorePointsBeException();
            }

            backupService.MergeRestorePoints(wastedRestorePoints);
        }
    }
}
