using System;
using Puppy.Monitoring.Builders;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring
{
    public class ReportInfoCollector
    {
        private readonly Report report;

        public ReportInfoCollector(bool success, Report report)
        {
            Success = success;

            eventBuilder = success
                               ? (IBuildReportingEvent)new SuccessEventReportingBuilder(this, null)
                               : new FailureEventReportingBuilder(this, null);

            this.report = report;
        }

        internal bool Success { get; set; }
        internal string Category { get; set; }
        internal string SubCategory { get; set; }
        internal bool IsTimeTracking { get; set; }
        internal long Milliseconds { get; set; }
        internal IBuildReportingEvent eventBuilder;
        internal Guid CorrelationId = Guid.Empty;

        public ReportInfoCollector(Report report, IEvent @event)
        {
            this.report = report;
            eventBuilder = new CustomEventReportingBuilder(this, @event);
        }

        internal string Segment { get; private set; }

        public ReportInfoCollector InCategory(string category)
        {
            Category = category;
            return this;
        }

        public ReportInfoCollector NoSubCatory()
        {
            return this;
        }

        public ReportInfoCollector InSubCategory(string subCategory)
        {
            SubCategory = subCategory;
            return this;
        }

        public ReportInfoCollector SegmentedAs(string segment)
        {
            this.Segment = segment;
            return this;
        }

        public ReportInfoCollector ItTook(long milliseconds)
        {
            IsTimeTracking = true;
            Milliseconds = milliseconds;
            eventBuilder = new TimeBasedEventReportingBuilder(this, eventBuilder);
            return this;
        }

        public ReportInfoCollector RelateTo(Guid correlationId)
        {
            this.CorrelationId = correlationId;
            return this;
        }

        public void Publish()
        {
            report.Publish(eventBuilder);
        }
    }
}