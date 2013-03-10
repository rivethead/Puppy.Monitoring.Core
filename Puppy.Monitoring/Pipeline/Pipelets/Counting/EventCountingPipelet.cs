using System.Collections.Generic;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets.Filters;

namespace Puppy.Monitoring.Pipeline.Pipelets.Counting
{
    public class EventCountingPipelet : BasePipelet
    {
        private static readonly ILog log = LogManager.GetLogger<EventCountingPipelet>();
        private readonly ICountEvents counter;

        public EventCountingPipelet(ICountEvents counter)
            : base(new IgnoreAdminEventsSpecification())
        {
            this.counter = counter;
        }

        protected override bool FilterEvent(IEvent @event)
        {
            return true;
        }

        protected override IEnumerable<IEvent> Accept(IEvent @event)
        {
            log.DebugFormat("Incrementing counter ({1}) for {0}", @event.GetType(), counter.GetType());
            counter.Increment(@event);
            return ListOfEvents.Create(new NumberOfEventsEvent(counter.Count));
        }
    }
}