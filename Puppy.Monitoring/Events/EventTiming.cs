using System;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class EventTiming
    {
        public DateTime PublishedOn { get; private set; }
        public TimeBuckets Buckets { get; private set; }

        public EventTiming(DateTime publishedOn)
        {
            PublishedOn = publishedOn;
            Buckets = new TimeBuckets(publishedOn);
        }

        public override string ToString()
        {
            return string.Format("Published on {0}{1}{2}",
                                 PublishedOn,
                                 Environment.NewLine,
                                 Buckets);

        }
    }
}