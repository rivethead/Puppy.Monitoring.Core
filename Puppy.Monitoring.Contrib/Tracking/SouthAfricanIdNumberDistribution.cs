using System;
using System.IO;
using System.Text.RegularExpressions;
using Puppy.Monitoring.Tracking;

namespace Puppy.Monitoring.Contrib.Tracking
{
    public class SouthAfricanIdNumberDistribution : IFileDistributionAlgorithm
    {
        private readonly SouthAfricanIdNumber idNumber;
        private string baseFolder;

        public SouthAfricanIdNumberDistribution(string idNumber) : this(idNumber, AppDomain.CurrentDomain.BaseDirectory)
        {
        }

        public SouthAfricanIdNumberDistribution(string idNumber, string baseFolder)
        {
            this.idNumber = new SouthAfricanIdNumber(idNumber);
            this.baseFolder = baseFolder;
        }
        public FileLocation GetFileLocation(string filename)
        {
            baseFolder = string.IsNullOrEmpty(baseFolder)
                             ? AppDomain.CurrentDomain.BaseDirectory
                             : baseFolder;

            return new FileLocation(filename.Trim(Path.GetInvalidPathChars()).Trim(Path.GetInvalidFileNameChars()),
                Path.Combine(baseFolder, idNumber.Checksum, idNumber.BirthMonth, idNumber.BirthDay, idNumber.IDNumber));
        }
    }

    internal class SouthAfricanIdNumber
    {
        public SouthAfricanIdNumber(string idNumber)
        {
            IDNumber = idNumber;
            ValidateIdNumber();
        }

        public string IDNumber { get; private set; }

        public string Checksum
        {
            get { return IDNumber.Substring(IDNumber.Length - 1); }
        }

        public string BirthMonth
        {
            get { return IDNumber.Substring(2, 2); }
        }

        public string BirthDay
        {
            get { return IDNumber.Substring(4, 2); }
        }

        private void ValidateIdNumber()
        {
            var errorMessage = string.Format("Invalid South Africa ID number '{0}'", IDNumber);

            if (string.IsNullOrEmpty(IDNumber))
                throw new Exception(errorMessage);

            const string pattern = @"^(\d{13})?$";

            if(!Regex.IsMatch(IDNumber, pattern))
                throw new Exception(errorMessage);
        }
    }
}