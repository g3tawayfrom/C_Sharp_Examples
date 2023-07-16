using Backups.FileSystems;
using Backups.StorageAlgorithms;

namespace Backups.Repositories
{
    public class Repository : IRepository
    {
        private List<string> storageList;
        private string savePath;

        public Repository(string path)
        {
            storageList = new List<string>();
            savePath = path;
        }

        public List<string> GetAllStorages()
        {
            return storageList;
        }

        public void Save(IStorageAlgorithm storageAlgorithm, IFileSystem fileSystem, int currentNumber, Guid id)
        {
            storageAlgorithm.Archivate(fileSystem, currentNumber, id, savePath);
            storageList.AddRange(storageAlgorithm.GetArchives());
        }
    }
}
