namespace Backups
{
    public class BackupObject
    {
        public BackupObject(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; }
    }
}
