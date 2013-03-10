using System;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class Timings
    {
        public Timings(long took)
        {
            Took = took;
        }

        public long Took { get; private set; }

        public override string ToString()
        {
            return string.Format("The event took {0} milliseconds", Took);
        }
    }
}