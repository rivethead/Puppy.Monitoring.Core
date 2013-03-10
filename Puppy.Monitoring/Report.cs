using Puppy.Monitoring.Builders;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring
{
    public class Report
    {
        public static ReportInfoCollector Success()
        {
            return new ReportInfoCollector(true, new Report());
        }

        public static ReportInfoCollector Failure()
        {
            return new ReportInfoCollector(false, new Report());
        }

        public static CustomReportInfoCollector CustomEvent()
        {
            return new CustomReportInfoCollector(new Report());
        }

        internal void Publish(IBuildReportingEvent builder)
        {
            var @event = builder.Build();

            new Publisher().Publish(@event);
        }
    }

}