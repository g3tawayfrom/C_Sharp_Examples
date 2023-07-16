namespace Backups.Extra.CleanerSystem
{
    public class DateLimit : ILimit
    {
        public DateLimit(DateTime limit)
        {
            Limit = limit;
            WastedRestorePoints = new List<RestorePointProxy>();
        }

        public DateTime Limit { get; set; }
        public List<RestorePointProxy> WastedRestorePoints { get; set; }

        public void LimitCheck(List<RestorePointProxy> restorePoints)
        {
            foreach (RestorePointProxy restorePoint in restorePoints)
            {
                if (restorePoint.CreationTime <= Limit)
                    WastedRestorePoints.Add(restorePoint);
            }
        }

        public List<RestorePointProxy> GetWastedRestorePoints()
        {
            return WastedRestorePoints;
        }
    }
}
