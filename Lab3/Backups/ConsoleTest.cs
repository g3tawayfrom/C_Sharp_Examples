using Backups.FileSystems;
using Backups.Repositories;
using Backups.StorageAlgorithms;

namespace Backups
{
    public class ConsoleTest
    {
        public static void Main()
        {
            string file1 = @"C:\Users\niggaWHAT\Documents\test1.txt";
            var backupObject1 = new BackupObject(file1);

            string file2 = @"C:\Users\niggaWHAT\Documents\test2.txt";
            var backupObject2 = new BackupObject(file2);

            var backupService = new BackupService(backupObject1);
            backupService.AddBackupObject(backupObject2);
            var repository = new Repository(@"C:\Users\niggaWHAT\Documents\Test");

            IStorageAlgorithm storageAlgorithm = new SingleAlgorithm();
            IFileSystem fileSystem = new RealSystem();

            backupService.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);
        }
    }
}
