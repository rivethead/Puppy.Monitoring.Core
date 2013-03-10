using System.IO;

namespace Puppy.Monitoring.Tracking
{
    public class SingleFolderFileDistribution : IFileDistributionAlgorithm
    {
        private readonly string baseFolder;
        private readonly string folderName;

        public SingleFolderFileDistribution(string folderName, string baseFolder)
        {
            this.folderName = folderName;
            this.baseFolder = baseFolder;
        }

        public FileLocation GetFileLocation(string filename)
        {
            return new FileLocation(filename.Trim(Path.GetInvalidPathChars()).Trim(Path.GetInvalidFileNameChars()),
                        Path.Combine(baseFolder, folderName));
        }
    }
}