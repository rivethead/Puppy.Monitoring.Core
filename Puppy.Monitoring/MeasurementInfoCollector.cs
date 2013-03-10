using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Puppy.Monitoring
{
    public class MeasurementInfoCollector
    {
        private Action actionToMeasure = () => { };
        private readonly List<Func<ReportInfoCollector>> failures = new List<Func<ReportInfoCollector>>();
        private readonly List<Func<ReportInfoCollector>> successes = new List<Func<ReportInfoCollector>>();

        public MeasurementInfoCollector(Action actionToMeasure)
        {
            this.actionToMeasure = actionToMeasure;
        }

        public MeasurementInfoCollector OnFailure(Func<ReportInfoCollector> failure)
        {
            this.failures.Add(failure);
            return this;
        }

        public MeasurementInfoCollector OnFailure(IEnumerable<Func<ReportInfoCollector>> failure)
        {
            this.failures.AddRange(failure);
            return this;
        }

        public MeasurementInfoCollector OnSuccess(Func<ReportInfoCollector> success)
        {
            this.successes.Add(success);
            return this;
        }

        public MeasurementInfoCollector OnSuccess(IEnumerable<Func<ReportInfoCollector>> success)
        {
            this.successes.AddRange(success);
            return this;
        }

        public void Gauge()
        {
            var stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();

                actionToMeasure();

                stopwatch.Stop();

                Execute(successes, stopwatch);
            }
            catch
            {
                stopwatch.Stop();
                Execute(failures, stopwatch);
                throw;
            }
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