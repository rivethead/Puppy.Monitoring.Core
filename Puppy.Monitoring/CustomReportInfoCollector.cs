using Puppy.Monitoring.Events;

namespace Puppy.Monitoring
{
    public class CustomReportInfoCollector
    {
        private readonly Report report;

        public CustomReportInfoCollector(Report report)
        {
            this.report = report;
        }

        public ReportInfoCollector Event(IEvent @event)
        {
            return new ReportInfoCollector(report, @event);
        }
    }
}