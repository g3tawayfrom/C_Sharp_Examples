namespace Backups.Extra.CleanerSystem
{
    public interface ISeacher
    {
        public void FindWastedRestorePoints(BackupServiceProxy backupService, List<ILimit> limits, List<RestorePointProxy> returnList);
    }
}
