namespace Backups.Extra.LoggerSystem
{
    public interface ILoggerSystem
    {
        void Record(string message, string? logFile);
    }
}
