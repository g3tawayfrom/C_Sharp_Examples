using System.IO.Compression;

namespace Backups.Extra.RestoreSystem
{
    public class DirectRestore : IRestoreSystem
    {
        public void Restore(RestorePointProxy restorePoint, string? newPath)
        {
            string restorePath = newPath + "Restored_" + restorePoint.CreationTime;

            Directory.CreateDirectory(restorePath);

            string repositoryPath = restorePoint.BackupStorage;
            var files = Directory.GetFiles(repositoryPath).ToList();

            foreach (string file in files)
            {
                ZipFile.ExtractToDirectory(file, restorePath);
            }
        }
    }
}
