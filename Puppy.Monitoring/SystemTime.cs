using System;

namespace Puppy.Monitoring
{
    public class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.UtcNow;
    }
}
