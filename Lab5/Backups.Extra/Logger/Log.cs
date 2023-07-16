using Backups.Extra.LoggerSystem;

namespace Backups.Extra.Logger
{
    public class Log : ILogger
    {
        public Log(ILoggerSystem loggerSystem, string? logFile = null)
        {
            LoggerSystem = loggerSystem;
            LogFile = logFile;
        }

        public ILoggerSystem LoggerSystem { get; }
        public string? LogFile { get; }

        public void StartProccess(string message)
        {
            Record("Started: " + message);
        }

        public void FinishProccess(string message)
        {
            Record("Finished: " + message);
        }

        public void ProccessInfo(string message)
        {
            Record(message);
        }

        public void ThrowInfo(string message)
        {
            Record(message);
        }

        public void Record(string message)
        {
            LoggerSystem.Record(message, LogFile);
        }
    }
}
