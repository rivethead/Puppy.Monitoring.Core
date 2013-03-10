using System.IO;

namespace Puppy.Monitoring.Tracking
{
    public class FileLocation
    {
        public string FileName { get; private set; }
        public string Folder { get; private set; }
        public string FullPath
        {
            get { return Path.Combine(Folder, FileName); }
        }

        public FileLocation(string fileName, string folder)
        {
            FileName = fileName;
            Folder = folder;
        }
    }

    public interface IFileDistributionAlgorithm
    {
        FileLocation GetFileLocation(string filename);
    }
}