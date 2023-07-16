using Backups.FileSystems;

namespace Backups.StorageAlgorithms
{
    public class SingleAlgorithm : IStorageAlgorithm
    {
        private List<string> incomingFiles;
        private List<string> archivatedFiles;

        public SingleAlgorithm()
        {
            incomingFiles = new List<string>();
            archivatedFiles = new List<string>();
        }

        public void AddIncomingFiles(List<string> incomingFiles)
        {
            this.incomingFiles.AddRange(incomingFiles);
        }

        public IReadOnlyList<string> GetArchives()
        {
            var outputArchives = new List<string>();
            foreach (string archive in archivatedFiles)
            {
                outputArchives.Add(archive);
            }

            archivatedFiles.Clear();
            return outputArchives;
        }

        public void Archivate(IFileSystem fileSystem, int currentNumber, Guid id, string outputPath)
        {
            fileSystem.SingleSave(incomingFiles, archivatedFiles, currentNumber, id, outputPath);
            incomingFiles.Clear();
        }
    }
}
