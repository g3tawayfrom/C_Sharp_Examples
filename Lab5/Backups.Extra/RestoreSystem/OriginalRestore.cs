using System.IO.Compression;
using Backups.Extra.Exceptions;

namespace Backups.Extra.RestoreSystem
{
    public class OriginalRestore : IRestoreSystem
    {
        public void Restore(RestorePointProxy restorePoint, string? newPath = null)
        {
            string? originPath = restorePoint.BackupObjectCollection[0].FilePath;

            if (originPath == null)
            {
                throw new NotExistsException();
            }

            originPath = originPath.Substring(0, originPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            string restorePath = originPath + "Restored_" + restorePoint.CreationTime;

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
