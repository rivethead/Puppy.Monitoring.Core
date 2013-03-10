using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Builders
{
    internal class FailureEventReportingBuilder : IBuildReportingEvent
    {
        private readonly ReportInfoCollector info;
        private readonly IBuildReportingEvent subBuilder;

        public FailureEventReportingBuilder(ReportInfoCollector info, IBuildReportingEvent subBuilder)
        {
            this.info = info;
            this.subBuilder = subBuilder;
        }

        public IEvent Build()
        {
            return subBuilder == null
                       ? new FailureEvent(new Categorisation(info.Category, info.SubCategory, info.Segment), info.CorrelationId)
                       : subBuilder.Build();
        }
    }
}