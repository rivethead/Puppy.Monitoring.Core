using System;
using System.Collections.Generic;

namespace Puppy.Monitoring
{
    public class TrackReportInfoCollector<TResponse>
    {
        private readonly List<Func<ReportInfoCollector>> failures = new List<Func<ReportInfoCollector>>();
        private readonly List<Func<ReportInfoCollector>> successes = new List<Func<ReportInfoCollector>>();
        private readonly TrackWritingInfoCollector<TResponse> trackWritingInfoCollector;

        public TrackReportInfoCollector(TrackWritingInfoCollector<TResponse> trackWritingInfoCollector)
        {
            this.trackWritingInfoCollector = trackWritingInfoCollector;
        }

        internal List<Func<ReportInfoCollector>> Successes
        {
            get { return successes; }
        }

        internal List<Func<ReportInfoCollector>> Failures
        {
            get { return failures; }
        }

        public TrackReportInfoCollector<TResponse> Report()
        {
            return this;
        }

        public TrackReportInfoCollector<TResponse> Success(Func<ReportInfoCollector> success)
        {
            successes.Add(success);
            return this;
        }

        public TrackReportInfoCollector<TResponse> Failure(Func<ReportInfoCollector> failure)
        {
            failures.Add(failure);
            return this;
        }

        public TResponse Go()
        {
            return trackWritingInfoCollector.Go();
        }
    }
}