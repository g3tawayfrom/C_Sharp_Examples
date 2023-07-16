using Backups.FileSystems;
using Backups.Repositories;
using Backups.StorageAlgorithms;

namespace Backups
{
    public class BackupService
    {
        public BackupService(BackupObject backupObject)
        {
            Backup = new Backup();
            BackupObjectList = new List<BackupObject> { backupObject };
            Id = Guid.NewGuid();
            CurrentNumber = 0;
        }

        public Backup Backup { get; }

        public List<BackupObject> BackupObjectList { get; }

        public Guid Id { get; }

        public int CurrentNumber { get; private set; }

        public void AddBackupObject(BackupObject backupObject)
        {
            BackupObjectList.Add(backupObject);
        }

        public void RemoveBackupObject(BackupObject backupObject)
        {
            BackupObjectList.Remove(backupObject);
        }

        public void AddNewRestorePoint(IStorageAlgorithm algorithm, IFileSystem fileSystem, IRepository repository)
        {
            var restorePoint = new RestorePoint(BackupObjectList);
            Backup.AddRestorePointToBackup(restorePoint);

            var workFiles = new List<string>();
            foreach (BackupObject backupObject in BackupObjectList)
            {
                workFiles.Add(backupObject.FilePath);
            }

            algorithm.AddIncomingFiles(workFiles);
            repository.Save(algorithm, fileSystem, CurrentNumber, Id);

            CurrentNumber++;
        }
    }
}
