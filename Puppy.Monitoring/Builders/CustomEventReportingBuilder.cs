using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Builders
{
    internal class CustomEventReportingBuilder : IBuildReportingEvent
    {
        private readonly ReportInfoCollector info;
        private readonly Event @event;

        public CustomEventReportingBuilder(ReportInfoCollector info, IEvent @event)
        {
            this.info = info;
            this.@event = @event as Event;
        }

        public IEvent Build()
        {
            @event.Categorisation = new Categorisation(info.Category, info.SubCategory, info.Segment);
            @event.CorrelationId = info.CorrelationId;
            @event.Timings = new Timings(info.Milliseconds);

            return @event;
        }
    }
}