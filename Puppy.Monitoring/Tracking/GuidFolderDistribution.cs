using System;
using System.IO;

namespace Puppy.Monitoring.Tracking
{
    public class GuidFolderDistribution : IFileDistributionAlgorithm
    {
        private readonly string baseFolder;
        private readonly Guid identifier;

        public GuidFolderDistribution(string baseFolder, Guid identifier)
        {
            this.baseFolder = string.IsNullOrEmpty(baseFolder) ? AppDomain.CurrentDomain.BaseDirectory : baseFolder;
            this.identifier = identifier;
        }

        public FileLocation GetFileLocation(string filename)
        {
            var identifierAsString = identifier.ToString();

            return new FileLocation(filename.Trim(Path.GetInvalidPathChars()).Trim(Path.GetInvalidFileNameChars()),
                                    Path.Combine(baseFolder, identifierAsString.Substring(0, 2), identifierAsString.Substring(2, 2), identifierAsString.Substring(4, 2)));
        }
    }
}