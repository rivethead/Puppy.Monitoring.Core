using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Puppy.Monitoring
{
    public class MeasurementInfoCollectorWithReturn<TReturn>
    {
        private readonly Func<TReturn> actionToMeasure = () => default(TReturn);
        private readonly List<Func<ReportInfoCollector>> failures = new List<Func<ReportInfoCollector>>();
        private readonly List<Func<ReportInfoCollector>> successes = new List<Func<ReportInfoCollector>>();

        public MeasurementInfoCollectorWithReturn(Func<TReturn> actionToMeasure)
        {
            this.actionToMeasure = actionToMeasure;
        }

        public MeasurementInfoCollectorWithReturn<TReturn> OnFailure(Func<ReportInfoCollector> failure)
        {
            this.failures.Add(failure);
            return this;
        }

        public MeasurementInfoCollectorWithReturn<TReturn> OnFailure(IEnumerable<Func<ReportInfoCollector>> failure)
        {
            this.failures.AddRange(failure);
            return this;
        }

        public MeasurementInfoCollectorWithReturn<TReturn> OnSuccess(IList<Func<ReportInfoCollector>> success)
        {
            this.successes.AddRange(success);
            return this;
        }

        public MeasurementInfoCollectorWithReturn<TReturn> OnSuccess(Func<ReportInfoCollector> success)
        {
            this.successes.Add(success);
            return this;
        }

        public TReturn Gauge()
        {
            var stopwatch = new Stopwatch();
            TReturn result = default(TReturn);
            try
            {
                stopwatch.Start();

                result = actionToMeasure();

                stopwatch.Stop();

                Execute(successes, stopwatch);
            }
            catch
            {
                stopwatch.Stop();
                Execute(failures, stopwatch);
                throw;
            }

            return result;
        }

        private void Execute(List<Func<ReportInfoCollector>> reporters, Stopwatch stopwatch)
        {
            if (!reporters.Any())
                return;

            foreach (var reporter in reporters)
            {
                reporter()
                    .ItTook(stopwatch.ElapsedMilliseconds)
                    .Publish();
            }
        }

    }
}