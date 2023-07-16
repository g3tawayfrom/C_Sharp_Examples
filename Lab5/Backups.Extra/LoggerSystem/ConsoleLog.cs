namespace Backups.Extra.LoggerSystem
{
    public class ConsoleLog : ILoggerSystem
    {
        public void Record(string message, string? logFile = null)
        {
            Console.WriteLine(message);
        }
    }
}
