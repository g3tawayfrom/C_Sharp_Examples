namespace Backups.Extra.CleanerSystem
{
    public interface IExterminator
    {
        public void ExterminateWastedRestorePoints(BackupServiceProxy backupService, List<RestorePointProxy> wastedRestorePoints);
    }
}
