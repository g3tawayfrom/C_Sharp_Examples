using Backups.FileSystems;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using Xunit;

namespace Backups.Tests
{
    public class BackupServiceTest
    {
        [Fact]
        public void AddBackup_SplitAndInMemory()
        {
            string file1 = @"C:\Users\niggaWHAT\Documents\test1.txt";
            var backupObject1 = new BackupObject(file1);

            string file2 = @"C:\Users\niggaWHAT\Documents\test2.txt";
            var backupObject2 = new BackupObject(file2);

            var backupService = new BackupService(backupObject1);
            backupService.AddBackupObject(backupObject2);
            var repository = new Repository(@"C:\Users\niggaWHAT\Documents\Test");

            IStorageAlgorithm storageAlgorithm = new SplitAlgorithm();
            IFileSystem fileSystem = new TestSystem();

            backupService.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            backupService.RemoveBackupObject(backupObject2);
            backupService.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            Assert.Equal(2, backupService.Backup.RestorePoints.Count);
            Assert.Equal(3, repository.GetAllStorages().Count);
        }
    }
}
