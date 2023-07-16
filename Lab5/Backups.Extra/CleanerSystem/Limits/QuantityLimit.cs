namespace Backups.Extra.CleanerSystem
{
    public class QuantityLimit : ILimit
    {
        public QuantityLimit(int limit)
        {
            Limit = limit;
            WastedRestorePoints = new List<RestorePointProxy>();
        }

        public int Limit { get; set; }
        public List<RestorePointProxy> WastedRestorePoints { get; set; }

        public void LimitCheck(List<RestorePointProxy> restorePoints)
        {
            int quantity = restorePoints.Count - Limit;

            for (int i = 0; i < quantity; i++)
            {
                WastedRestorePoints.Add(restorePoints[i]);
            }
        }

        public List<RestorePointProxy> GetWastedRestorePoints()
        {
            return WastedRestorePoints;
        }
    }
}
