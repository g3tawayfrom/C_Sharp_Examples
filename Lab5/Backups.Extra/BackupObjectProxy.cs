namespace Backups.Extra
{
    public class BackupObjectProxy
    {
        public BackupObjectProxy() { }

        public BackupObjectProxy(string filePath)
        {
            FilePath = filePath;
        }

        public string? FilePath { get; set; }
    }
}
