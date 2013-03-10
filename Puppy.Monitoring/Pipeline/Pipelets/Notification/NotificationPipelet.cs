using System.Collections.Generic;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets.Filters;

namespace Puppy.Monitoring.Pipeline.Pipelets.Notification
{
    public class NotificationPipelet : BasePipelet
    {
        private static readonly ILog log = LogManager.GetLogger<NotificationPipelet>();
        private readonly IEventSpecification filter;
        private readonly INotifyBasedOnEvent notifier;

        public NotificationPipelet(INotifyBasedOnEvent notifier, IEventSpecification filter) 
            : base(new NotSpecificEventTypeSpecification(typeof(NotificationRaisedEvent)))
        {
            this.notifier = notifier;
            this.filter = filter;
        }

        protected override bool FilterEvent(IEvent @event)
        {
            return filter.SatisfiedBy(@event);
        }

        protected override IEnumerable<IEvent> Accept(IEvent @event)
        {
            log.DebugFormat("Notifier ({2}) invoked because {0} satisfied {1}", @event.GetType(), filter.GetType(),
                            notifier.GetType());
            notifier.Raise(@event);

            return ListOfEvents.Create(new NotificationRaisedEvent(@event));
        }
    }
}