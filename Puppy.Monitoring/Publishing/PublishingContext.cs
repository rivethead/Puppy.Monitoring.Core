using System;

namespace Puppy.Monitoring.Publishing
{
    [Serializable]
    public class PublishingContext
    {
        public PublishingContext(string system, string module, string machineName, string runningAs)
        {
            System = system;
            Module = module;
            MachineName = machineName;
            RunningAs = runningAs;
        }

        public PublishingContext(string system, string module)
            : this(system, module, Environment.MachineName, Environment.UserName)
        {
        }

        public string Module { get; private set; }
        public string MachineName { get; private set; }
        public string RunningAs { get; private set; }
        public string System { get; private set; }
    }
}