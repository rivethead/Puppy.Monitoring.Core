using System.Collections.Generic;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets.Filters;

namespace Puppy.Monitoring.Pipeline.Pipelets
{
    public abstract class BasePipelet : IPipelet
    {
        private static readonly ILog log = LogManager.GetLogger<BasePipelet>();
        protected readonly IEventSpecification eventFilter;

        protected BasePipelet() : this(new AlwaysSatisfiedEventSpecification())
        {
        }

        protected BasePipelet(IEventSpecification eventFilter)
        {
            this.eventFilter = eventFilter;
        }

        public IEnumerable<IEvent> Flow(IEvent @event)
        {
            log.InfoFormat("Received {0} in pipelet {1}", @event.GetType(), this.GetType());

            if (!WantsEvent(@event))
            {
                log.InfoFormat("Ignoring event {0} because filter is not satisfied", @event.GetType());

                return ListOfEvents.EmptyList();
            }

            return Accept(@event);
        }

        public bool WantsEvent(IEvent @event)
        {
            return eventFilter.SatisfiedBy(@event) && FilterEvent(@event);
        }

        protected abstract bool FilterEvent(IEvent @event);

        protected abstract IEnumerable<IEvent> Accept(IEvent @event);
    }
}