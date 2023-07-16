namespace Backups.Extra.CleanerSystem
{
    public interface IGrouper
    {
        public void GroupWastedRestorePoints(ISeacher? seacher, int? limitNumber, BackupServiceProxy backupService, List<ILimit> limits, List<RestorePointProxy> returnList);

        public void ExterminateWastedRestorePoints(IExterminator exterminator, BackupServiceProxy backupService, List<RestorePointProxy> wastedRestorePoints);
    }
}
