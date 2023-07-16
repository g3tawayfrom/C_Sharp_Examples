using Backups.Extra.Exceptions;
using Backups.Extra.Logger;
using Backups.Extra.LoggerSystem;
using Backups.Extra.RestoreSystem;
using Backups.FileSystems;
using Backups.Repositories;
using Backups.StorageAlgorithms;

namespace Backups.Extra
{
    public class BackupServiceProxy
    {
        public BackupServiceProxy() { }

        public BackupServiceProxy(Backups.BackupObject backupObject, ILogger logger)
        {
            BasicService = new Backups.BackupService(backupObject);

            BackupObjectsList = new List<BackupObjectProxy>();
            foreach (Backups.BackupObject basicBackupObject in BasicService.BackupObjectList)
            {
                BackupObjectsList.Add(new BackupObjectProxy(basicBackupObject.FilePath));
            }

            Id = BasicService.Id;
            CurrentNumber = BasicService.CurrentNumber;
            Backup = new BackupProxy();
            Logger = logger;
        }

        public Backups.BackupService BasicService { get; } = new Backups.BackupService(new Backups.BackupObject(string.Empty));
        public List<BackupObjectProxy> BackupObjectsList { get; set; } = new List<BackupObjectProxy>();
        public Guid Id { get; set; }
        public int CurrentNumber { get; set; }
        public BackupProxy Backup { get; set; } = new BackupProxy();
        public ILogger Logger { get; } = new Log(new ConsoleLog());

        public void AddBackupObject(Backups.BackupObject backupObject)
        {
            Logger.StartProccess("AddBackupObject");
            Logger.ProccessInfo($"{backupObject} - incoming files");

            Logger.StartProccess("BasicService.AddBackupObject");
            Logger.ProccessInfo($"{backupObject} - incoming files");
            BasicService.AddBackupObject(backupObject);
            Logger.FinishProccess("BasicService.AddBackupObject");

            Logger.FinishProccess("AddBackupObject");
        }

        public void RemoveBackupObject(Backups.BackupObject backupObject)
        {
            Logger.StartProccess("RemoveBackupObject");
            Logger.ProccessInfo($"{backupObject} - incoming files");

            Logger.StartProccess("BasicService.RemoveBackupObject");
            Logger.ProccessInfo($"{backupObject} - incoming files");
            BasicService.RemoveBackupObject(backupObject);
            Logger.FinishProccess("BasicService.RemoveBackupObject");

            Logger.FinishProccess("RemoveBackupObject");
        }

        public void AddNewRestorePoint(IStorageAlgorithm algorithm, IFileSystem fileSystem, IRepository repository)
        {
            Logger.StartProccess("AddNewRestorePoint");
            Logger.ProccessInfo($"{algorithm}, {fileSystem}, {repository} - incoming files");

            Logger.StartProccess("BasicService.AddNewRestorePoint");
            Logger.ProccessInfo($"{algorithm}, {fileSystem}, {repository} - incoming files");
            BasicService.AddNewRestorePoint(algorithm, fileSystem, repository);
            Logger.FinishProccess("BasicService.AddNewRestorePoint");

            Logger.StartProccess("new RestorePoint");
            Logger.ProccessInfo($"{BasicService.BackupObjectList} - incoming files");
            var restorePoint = new RestorePointProxy(BasicService.BackupObjectList);
            Logger.FinishProccess("new RestorePoint");

            Logger.StartProccess("repository.GetAllStorages");
            List<string> strorages = repository.GetAllStorages();
            Logger.FinishProccess("repository.GetAllStorages");

            Logger.StartProccess("strorages.Last().Substring");
            Logger.ProccessInfo($"{strorages} - incoming files");
            restorePoint.BackupStorage = strorages.Last().Substring(0, strorages.Last().LastIndexOf(Path.DirectorySeparatorChar));
            Logger.FinishProccess("strorages.Last().Substring");

            Logger.StartProccess("AddRestorePointToBackup");
            Logger.ProccessInfo($"{restorePoint} - incoming files");
            Backup.AddRestorePointToBackup(restorePoint);
            Logger.FinishProccess("AddRestorePointToBackup");

            Logger.FinishProccess("AddNewRestorePoint");
        }

        public void RestoreObjectsFromRestorePoint(IRestoreSystem restoreSystem, int index, string? newPath = null)
        {
            Logger.StartProccess("RestoreObjectsFromRestorePoint");
            Logger.ProccessInfo($"{restoreSystem}, {index}, {newPath} - incoming files");

            Logger.StartProccess("Backup.RestorePoints");
            Logger.ProccessInfo($"{index} - incoming files");
            RestorePointProxy restorePoint = Backup.RestorePoints[index];
            Logger.FinishProccess("Backup.RestorePoints");

            Logger.StartProccess("restoreSystem.Restore");
            Logger.ProccessInfo($"{restorePoint}, {newPath} - incoming files");
            restoreSystem.Restore(restorePoint, newPath);
            Logger.FinishProccess("restoreSystem.Restore");

            Logger.FinishProccess("RestoreObjectsFromRestorePoint");
        }

        public void MergeRestorePoints(List<RestorePointProxy> wastedRestorePoints)
        {
            Logger.StartProccess("MergeRestorePoints");
            Logger.ProccessInfo($"{wastedRestorePoints} - incoming files");

            Logger.StartProccess("Backup.RestorePoints.Count");
            int limit = Backup.RestorePoints.Count();
            Logger.FinishProccess("Backup.RestorePoints.Count");

            for (int i = 0; i < limit - 1; i++)
            {
                Logger.StartProccess("Backup.RestorePoints");
                Logger.ProccessInfo($"{i} - incoming files");
                RestorePointProxy oldRestorePoint = Backup.RestorePoints[i];
                Logger.FinishProccess("Backup.RestorePoints");

                Logger.StartProccess("Backup.RestorePoints");
                Logger.ProccessInfo($"{i + 1} - incoming files");
                RestorePointProxy newRestorePoint = Backup.RestorePoints[i + 1];
                Logger.FinishProccess("Backup.RestorePoints");

                Logger.StartProccess("wastedRestorePoints.Contains");
                Logger.ProccessInfo($"{Backup.RestorePoints[i]} - incoming files");
                if (!wastedRestorePoints.Contains(Backup.RestorePoints[i]))
                {
                    Logger.FinishProccess("wastedRestorePoints.Contains");
                    continue;
                }

                Logger.FinishProccess("wastedRestorePoints.Contains");

                if (oldRestorePoint.BackupStorage == null)
                {
                    Logger.ThrowInfo("NotExistsException - oldRestorePoint.BackupStorage");
                    throw new NotExistsException();
                }

                if (newRestorePoint.BackupStorage == null)
                {
                    Logger.ThrowInfo("NotExistsException - newRestorePoint.BackupStorage");
                    throw new NotExistsException();
                }

                Logger.StartProccess("new DirectoryInfo().GetFiles().All");
                Logger.ProccessInfo($"{oldRestorePoint.BackupStorage} - incoming files");
                if (!new DirectoryInfo(oldRestorePoint.BackupStorage).GetFiles("*.zip").All(f => f.Extension == ".zip"))
                {
                    Logger.FinishProccess("new DirectoryInfo().GetFiles().All");

                    Logger.StartProccess("Backup.RestorePoints.Remove");
                    Logger.ProccessInfo($"{oldRestorePoint} - incoming files");
                    Backup.RestorePoints.Remove(oldRestorePoint);
                    Logger.FinishProccess("Backup.RestorePoints.Remove");
                    continue;
                }

                Logger.StartProccess("Directory.GetFiles().ToList");
                Logger.ProccessInfo($"{oldRestorePoint.BackupStorage} - incoming files");
                var oldStorages = Directory.GetFiles(oldRestorePoint.BackupStorage).ToList();
                Logger.FinishProccess("Directory.GetFiles().ToList");

                foreach (string storage in oldStorages)
                {
                    Logger.StartProccess("storage.Substring");
                    Logger.ProccessInfo($"{storage.LastIndexOf(Path.DirectorySeparatorChar) + 1} - incoming files");
                    string file = storage.Substring(storage.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                    Logger.FinishProccess("storage.Substring");

                    Logger.StartProccess("storage.Substring");
                    Logger.ProccessInfo($"{storage.LastIndexOf(Path.DirectorySeparatorChar) + 1} - incoming files");
                    var newStorages = Directory.GetFiles(newRestorePoint.BackupStorage).ToList();
                    Logger.FinishProccess("storage.Substring");

                    Logger.StartProccess("newStorages.Exists");
                    Logger.ProccessInfo($"{file} - incoming files");
                    if (!newStorages.Exists(s => s.Contains(file)))
                    {
                        Logger.FinishProccess("newStorages.Exists");

                        string newPath = newRestorePoint.BackupStorage + @"\";

                        Logger.StartProccess("File.Copy");
                        Logger.ProccessInfo($"{storage}, {newPath} - incoming files");
                        File.Copy(storage, newPath);
                        Logger.FinishProccess("File.Copy");
                    }
                }

                Backup.RestorePoints.Remove(oldRestorePoint);
            }

            Logger.StartProccess("wastedRestorePoints.Clear");
            wastedRestorePoints.Clear();
            Logger.FinishProccess("wastedRestorePoints.Clear");

            Logger.FinishProccess("MergeRestorePoints");
        }

        public void DeleteRestorePoints(List<RestorePointProxy> wastedRestorePoints)
        {
            Logger.StartProccess("DeleteRestorePoints");
            Logger.ProccessInfo($"{wastedRestorePoints} - incoming files");

            foreach (RestorePointProxy wastedRestorePoint in wastedRestorePoints)
            {
                Logger.StartProccess("Backup.RestorePoints.Remove");
                Logger.ProccessInfo($"{wastedRestorePoints} - incoming files");
                Backup.RestorePoints.Remove(wastedRestorePoint);
                Logger.FinishProccess("Backup.RestorePoints.Remove");
            }

            Logger.StartProccess("wastedRestorePoints.Clear");
            wastedRestorePoints.Clear();
            Logger.FinishProccess("wastedRestorePoints.Clear");

            Logger.FinishProccess("DeleteRestorePoints");
        }
    }
}
