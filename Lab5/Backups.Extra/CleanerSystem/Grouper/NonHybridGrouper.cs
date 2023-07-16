using Backups.Extra.Exceptions;

namespace Backups.Extra.CleanerSystem
{
    public class NonHybridGrouper : IGrouper
    {
        public void GroupWastedRestorePoints(ISeacher? seacher, int? limitNumber, BackupServiceProxy backupService, List<ILimit> limits, List<RestorePointProxy> returnList)
        {
            if (limitNumber == null)
            {
                throw new NotExistsException();
            }

            ILimit limit = limits[(int)limitNumber];

            limit.LimitCheck(backupService.Backup.RestorePoints);

            foreach (RestorePointProxy wastedRestorePoint in limit.GetWastedRestorePoints())
            {
                if (!returnList.Contains(wastedRestorePoint))
                {
                    returnList.Add(wastedRestorePoint);
                }
            }
        }

        public void ExterminateWastedRestorePoints(IExterminator exterminator, BackupServiceProxy backupService, List<RestorePointProxy> wastedRestorePoints)
        {
            exterminator.ExterminateWastedRestorePoints(backupService, wastedRestorePoints);
        }
    }
}
