using System;

namespace Puppy.Monitoring
{
    public class Measure
    {
        public static MeasurementInfoCollectorWithReturn<TReturn> This<TReturn>(Func<TReturn> action)
        {
            return new MeasurementInfoCollectorWithReturn<TReturn>(action);
        }

        public static MeasurementInfoCollector This(Action action)
        {
            return new MeasurementInfoCollector(action);
        }
    }
}