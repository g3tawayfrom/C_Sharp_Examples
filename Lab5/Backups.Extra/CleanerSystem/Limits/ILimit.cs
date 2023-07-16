namespace Backups.Extra.CleanerSystem
{
    public interface ILimit
    {
        public void LimitCheck(List<RestorePointProxy> restorePoints);

        public List<RestorePointProxy> GetWastedRestorePoints();
    }
}
