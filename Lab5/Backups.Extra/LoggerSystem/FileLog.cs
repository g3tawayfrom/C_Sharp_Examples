using Backups.Extra.Exceptions;

namespace Backups.Extra.LoggerSystem
{
    public class FileLog : ILoggerSystem
    {
        public void Record(string message, string? logFile)
        {
            if (logFile == null)
            {
                throw new NotExistsException();
            }

            var record = new StreamWriter(logFile);
            record.WriteLine(message);
            record.Close();
        }
    }
}
