using System;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class TimeBuckets
    {
        public TimeBuckets(DateTime dateTime)
        {
            Year = dateTime.Year;
            Month = dateTime.Month;
            Day = dateTime.Day;
            Hour = dateTime.Hour;
            Minute = dateTime.Minute;
            Second = dateTime.Second;
            UnixTimestamp = (dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime()).TotalSeconds;
        }

        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Second { get; private set; }
        public double UnixTimestamp { get; private set; }

        public override string ToString()
        {
            return string.Format("Buckets: {0}/{1}/{2}/{3}/{4}/{5}",
                Year,
                Month,
                Day,
                Hour,
                Minute,
                Second);
        }
    }
}