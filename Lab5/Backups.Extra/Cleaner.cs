using Backups.Extra.CleanerSystem;

namespace Backups.Extra
{
    public class Cleaner
    {
        public Cleaner(List<ILimit> limits)
        {
            Limits = limits;
            WastedRestorePoints = new List<RestorePointProxy>();
        }

        public List<ILimit> Limits { get; set; }
        public List<RestorePointProxy> WastedRestorePoints { get; set; }

        public void ClearRestorePoints(IGrouper grouper, ISeacher? seacher, IExterminator exterminator, int? limitNumber, BackupServiceProxy backupService)
        {
            grouper.GroupWastedRestorePoints(seacher, limitNumber, backupService, Limits, WastedRestorePoints);
            exterminator.ExterminateWastedRestorePoints(backupService, WastedRestorePoints);
        }
    }
}
