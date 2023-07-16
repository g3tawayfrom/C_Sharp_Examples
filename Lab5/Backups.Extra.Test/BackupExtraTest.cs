using Backups.Extra.CleanerSystem;
using Backups.Extra.Exceptions;
using Backups.Extra.Logger;
using Backups.Extra.LoggerSystem;
using Backups.FileSystems;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using Xunit;

namespace Backups.Extra.Test
{
    public class BackupExtraTest
    {
        [Fact]
        public void OptimizeRestorePoints_NonHybrid()
        {
            string file1 = @"C:\Users\niggaWHAT\Documents\test1.txt";
            var backupObject1 = new Backups.BackupObject(file1);

            string file2 = @"C:\Users\niggaWHAT\Documents\test2.txt";
            var backupObject2 = new Backups.BackupObject(file2);

            var logger = new Log(new ConsoleLog());

            var backupService = new BackupServiceProxy(backupObject1, logger);
            backupService.AddBackupObject(backupObject2);

            var repository = new Repository(@"C:\Users\niggaWHAT\Documents\Test");

            IStorageAlgorithm storageAlgorithm = new SplitAlgorithm();
            IFileSystem fileSystem = new TestSystem();

            backupService.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            backupService.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            backupService.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            var limits = new List<ILimit>();

            limits.Add(new QuantityLimit(3));
            limits.Add(new DateLimit(DateTime.Now));

            backupService.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            backupService.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            var cleaner = new Cleaner(limits);
            cleaner.ClearRestorePoints(new NonHybridGrouper(), null, new Deleter(), 0, backupService);

            if (backupService.Backup == null)
            {
                throw new Exception();
            }

            Assert.Equal(3, backupService.Backup.RestorePoints.Count);

            cleaner.ClearRestorePoints(new NonHybridGrouper(), null, new Deleter(), 1, backupService);

            Assert.Equal(2, backupService.Backup.RestorePoints.Count);
        }

        [Fact]
        public void OptimizeRestorePoints_AllHybrid()
        {
            string file1 = @"C:\Users\niggaWHAT\Documents\test1.txt";
            var backupObject1 = new Backups.BackupObject(file1);

            string file2 = @"C:\Users\niggaWHAT\Documents\test2.txt";
            var backupObject2 = new Backups.BackupObject(file2);

            var logger = new Log(new ConsoleLog());

            var backupService1 = new BackupServiceProxy(backupObject1, logger);
            backupService1.AddBackupObject(backupObject2);

            var repository = new Repository(@"C:\Users\niggaWHAT\Documents\Test");

            IStorageAlgorithm storageAlgorithm = new SplitAlgorithm();
            IFileSystem fileSystem = new TestSystem();

            backupService1.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            backupService1.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            backupService1.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            var limits = new List<ILimit>();

            limits.Add(new QuantityLimit(2));
            limits.Add(new DateLimit(DateTime.Now));

            backupService1.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            var cleaner = new Cleaner(limits);
            cleaner.ClearRestorePoints(new HybridGrouper(), new AllHybridSeacher(), new Deleter(), null, backupService1);

            if (backupService1.Backup == null)
            {
                throw new Exception();
            }

            Assert.Equal(2, backupService1.Backup.RestorePoints.Count);
        }

        [Fact]
        public void OptimizeRestorePoints_AnyHybrid()
        {
            string file1 = @"C:\Users\niggaWHAT\Documents\test1.txt";
            var backupObject1 = new Backups.BackupObject(file1);

            string file2 = @"C:\Users\niggaWHAT\Documents\test2.txt";
            var backupObject2 = new Backups.BackupObject(file2);

            var logger = new Log(new ConsoleLog());

            var backupService1 = new BackupServiceProxy(backupObject1, logger);
            backupService1.AddBackupObject(backupObject2);

            var repository = new Repository(@"C:\Users\niggaWHAT\Documents\Test");

            IStorageAlgorithm storageAlgorithm = new SplitAlgorithm();
            IFileSystem fileSystem = new TestSystem();

            backupService1.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            backupService1.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            backupService1.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            var limits = new List<ILimit>();

            limits.Add(new QuantityLimit(3));
            limits.Add(new DateLimit(DateTime.Now));

            backupService1.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            var cleaner = new Cleaner(limits);
            cleaner.ClearRestorePoints(new HybridGrouper(), new AnyHybridSeacher(), new Deleter(), null, backupService1);

            if (backupService1.Backup == null)
            {
                throw new Exception();
            }

            Assert.Single(backupService1.Backup.RestorePoints);
        }

        [Fact]
        public void CantDeleteAllRestorePoints_ThrowException()
        {
            string file1 = @"C:\Users\niggaWHAT\Documents\test1.txt";
            var backupObject1 = new Backups.BackupObject(file1);

            string file2 = @"C:\Users\niggaWHAT\Documents\test2.txt";
            var backupObject2 = new Backups.BackupObject(file2);

            var logger = new Log(new ConsoleLog());

            var backupService1 = new BackupServiceProxy(backupObject1, logger);
            backupService1.AddBackupObject(backupObject2);

            var repository = new Repository(@"C:\Users\niggaWHAT\Documents\Test");

            IStorageAlgorithm storageAlgorithm = new SplitAlgorithm();
            IFileSystem fileSystem = new TestSystem();

            backupService1.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            backupService1.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            backupService1.AddNewRestorePoint(storageAlgorithm, fileSystem, repository);

            var limits = new List<ILimit>();

            limits.Add(new QuantityLimit(3));
            limits.Add(new DateLimit(DateTime.Now));

            var cleaner = new Cleaner(limits);

            if (backupService1.Backup == null)
            {
                throw new Exception();
            }

            Assert.Throws<NoRestorePointsBeException>(() => cleaner.ClearRestorePoints(new NonHybridGrouper(), null, new Deleter(), 1, backupService1));
        }
    }
}
