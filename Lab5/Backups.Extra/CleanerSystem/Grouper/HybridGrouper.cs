using Backups.Extra.Exceptions;

namespace Backups.Extra.CleanerSystem
{
    public class HybridGrouper : IGrouper
    {
        public void GroupWastedRestorePoints(ISeacher? seacher, int? limitNumber, BackupServiceProxy backupService, List<ILimit> limits, List<RestorePointProxy> returnList)
        {
            if (seacher == null)
            {
                throw new NotExistsException();
            }

            seacher.FindWastedRestorePoints(backupService, limits, returnList);
        }

        public void ExterminateWastedRestorePoints(IExterminator exterminator, BackupServiceProxy backupService, List<RestorePointProxy> wastedRestorePoints)
        {
            exterminator.ExterminateWastedRestorePoints(backupService, wastedRestorePoints);
        }
    }
}
