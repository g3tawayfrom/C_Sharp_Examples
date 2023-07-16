using Backups.FileSystems;
using Backups.StorageAlgorithms;

namespace Backups.Repositories
{
    public interface IRepository
    {
        List<string> GetAllStorages();

        void Save(IStorageAlgorithm storageAlgorithm, IFileSystem fileSystem, int currentNumber, Guid id);
    }
}
