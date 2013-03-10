using System.Collections.Generic;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets.Filters;

namespace Puppy.Monitoring.Pipeline.Pipelets.Counting
{
    public class SpecificEventCountingPipelet : BasePipelet
    {
        private static readonly ILog log = LogManager.GetLogger<SpecificEventCountingPipelet>();
        private readonly ICountEvents counter;
        private readonly IEventSpecification filter;

        public SpecificEventCountingPipelet(ICountEvents counter, IEventSpecification filter)
            : base(new IgnoreAdminEventsSpecification())
        {
            this.counter = counter;
            this.filter = filter;
        }

        protected override bool FilterEvent(IEvent @event)
        {
            return filter.SatisfiedBy(@event);
        }

        protected override IEnumerable<IEvent> Accept(IEvent @event)
        {
            log.DebugFormat("Incrementing counter ({2}) because {0} satisfied {1}", @event.GetType(), filter.GetType(),
                            counter.GetType());
            counter.Increment(@event);

            return ListOfEvents.Create(new NumberOfSpecificEventEvent(counter.Count, @event));
        }
    }
}