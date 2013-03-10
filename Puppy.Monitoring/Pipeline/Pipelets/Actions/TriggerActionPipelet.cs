using System.Collections.Generic;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets.Filters;

namespace Puppy.Monitoring.Pipeline.Pipelets.Actions
{
    public class TriggerActionPipelet : BasePipelet
    {
        private static readonly ILog log = LogManager.GetLogger<TriggerActionPipelet>();
        private readonly IEventSpecification filter;
        private readonly ITriggerActionBasedOnEvent trigger;

        public TriggerActionPipelet(ITriggerActionBasedOnEvent trigger, IEventSpecification filter)
            : base(new NotSpecificEventTypeSpecification(typeof (ActionTriggeredEvent)))
        {
            this.trigger = trigger;
            this.filter = filter;
        }

        protected override bool FilterEvent(IEvent @event)
        {
            return filter.SatisfiedBy(@event);
        }

        protected override IEnumerable<IEvent> Accept(IEvent @event)
        {
            log.DebugFormat("Trigger ({2}) invoked because {0} satisfied {1}", @event.GetType(), filter.GetType(),
                            trigger.GetType());
            trigger.Trigger(@event);

            return ListOfEvents.Create(new ActionTriggeredEvent(@event));
        }
    }

}