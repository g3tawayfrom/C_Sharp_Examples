namespace Backups.FileSystems
{
    public interface IFileSystem
    {
        public void SingleSave(List<string> incomingFiles, List<string> archivatedFiles, int currentNumber, Guid id, string outputPath);

        public void SplitSave(List<string> incomingFiles, List<string> archivatedFiles, int currentNumber, Guid id, string outputPath);
    }
}
