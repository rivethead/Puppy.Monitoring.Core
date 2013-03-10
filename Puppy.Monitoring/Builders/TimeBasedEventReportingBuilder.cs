using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Builders
{
    internal class TimeBasedEventReportingBuilder : IBuildReportingEvent
    {
        private readonly ReportInfoCollector info;
        private readonly IBuildReportingEvent baseBuilder;

        public TimeBasedEventReportingBuilder(ReportInfoCollector info, IBuildReportingEvent baseBuilder)
        {
            this.info = info;
            this.baseBuilder = baseBuilder;
        }

        public IEvent Build()
        {
            var @event = baseBuilder.Build() as Event;

            @event.Timings = new Timings(info.Milliseconds);

            return @event;
        }
    }
}