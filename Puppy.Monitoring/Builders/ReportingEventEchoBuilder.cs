using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Builders
{
    internal class ReportingEventEchoBuilder : IBuildReportingEvent
    {
        private readonly IEvent @event;

        public ReportingEventEchoBuilder(IEvent @event)
        {
            this.@event = @event;
        }

        public IEvent Build()
        {
            return @event;
        }
    }
}