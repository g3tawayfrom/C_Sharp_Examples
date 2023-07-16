namespace Backups.FileSystems
{
    public class TestSystem : IFileSystem
    {
        public void SingleSave(List<string> incomingFiles, List<string> archivatedFiles, int currentNumber, Guid id, string outputPath)
        {
            string storePath = outputPath + Path.DirectorySeparatorChar + "RestorePoint" + currentNumber + ".zip";
            archivatedFiles.Add(storePath);
        }

        public void SplitSave(List<string> incomingFiles, List<string> archivatedFiles, int currentNumber, Guid id, string outputPath)
        {
            string storePath = outputPath + Path.DirectorySeparatorChar + id + Path.DirectorySeparatorChar + "RestorePoint" + currentNumber;
            foreach (string filePath in incomingFiles)
            {
                string fileName = filePath.Substring(filePath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                archivatedFiles.Add(storePath + Path.DirectorySeparatorChar + fileName.Substring(0, fileName.LastIndexOf('.')) + ".zip");
            }
        }
    }
}
