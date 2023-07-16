using Backups.FileSystems;

namespace Backups.StorageAlgorithms
{
    public interface IStorageAlgorithm
    {
        public IReadOnlyList<string> GetArchives();

        public void AddIncomingFiles(List<string> incomingFiles);

        public void Archivate(IFileSystem fileSystem, int currentNumber, Guid id, string outputPath);
    }
}
