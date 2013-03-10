using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Builders
{
    internal class SuccessEventReportingBuilder : IBuildReportingEvent
    {
        private readonly ReportInfoCollector info;
        private readonly IBuildReportingEvent subBuilder;

        public SuccessEventReportingBuilder(ReportInfoCollector info, IBuildReportingEvent subBuilder)
        {
            this.info = info;
            this.subBuilder = subBuilder;
        }

        public IEvent Build()
        {
            return subBuilder != null
                       ? subBuilder.Build()
                       : new SuccessEvent(new Categorisation(info.Category, info.SubCategory, info.Segment), info.CorrelationId);
        }
    }
}