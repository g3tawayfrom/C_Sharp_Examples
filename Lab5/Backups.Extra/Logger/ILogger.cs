namespace Backups.Extra.Logger
{
    public interface ILogger
    {
        void StartProccess(string message);

        void FinishProccess(string message);

        void ProccessInfo(string message);

        void ThrowInfo(string message);

        void Record(string message);
    }
}
