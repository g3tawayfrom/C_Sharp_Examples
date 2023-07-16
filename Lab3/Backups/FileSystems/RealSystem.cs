using System.IO.Compression;

namespace Backups.FileSystems
{
    public class RealSystem : IFileSystem
    {
        public void SingleSave(List<string> incomingFiles, List<string> archivatedFiles, int currentNumber, Guid id, string outputPath)
        {
            string storePath = outputPath + Path.DirectorySeparatorChar + id + Path.DirectorySeparatorChar + "RestorePoint" + currentNumber;
            Directory.CreateDirectory(storePath);
            foreach (string filePath in incomingFiles)
            {
                string fileName = filePath.Substring(filePath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                File.Copy(filePath, storePath + Path.DirectorySeparatorChar + fileName);
            }

            ZipFile.CreateFromDirectory(storePath, storePath + ".zip");
            archivatedFiles.Add(storePath + ".zip");
            Directory.Delete(storePath, true);
        }

        public void SplitSave(List<string> incomingFiles, List<string> archivatedFiles, int currentNumber, Guid id, string outputPath)
        {
            string storePath = outputPath + Path.DirectorySeparatorChar + id + Path.DirectorySeparatorChar + "RestorePoint" + currentNumber;
            Directory.CreateDirectory(storePath);
            foreach (string filePath in incomingFiles)
            {
                string fileName = filePath.Substring(filePath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                string tempPath = storePath + Path.DirectorySeparatorChar + fileName.Substring(0, fileName.LastIndexOf('.'));
                Directory.CreateDirectory(tempPath);
                File.Copy(filePath, tempPath + Path.DirectorySeparatorChar + fileName);
                ZipFile.CreateFromDirectory(tempPath, tempPath + ".zip");
                archivatedFiles.Add(tempPath + ".zip");
                Directory.Delete(tempPath, true);
            }
        }
    }
}
