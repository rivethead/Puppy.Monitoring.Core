using System.Collections.Generic;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets.Filters;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Pipeline.Pipelets.Publishing
{
    public class PublishingPipelet : BasePipelet
    {
        private static readonly ILog log = LogManager.GetLogger<PublishingPipelet>();
        private readonly IPublishEvents publisher;

        public PublishingPipelet(IPublishEvents publisher, IEventSpecification eventFilter) : base(eventFilter)
        {
            this.publisher = publisher;
        }

        protected override bool FilterEvent(IEvent @event)
        {
            return true;
        }

        protected override IEnumerable<IEvent> Accept(IEvent @event)
        {
            log.DebugFormat("Publishing {0} to {1} publiser", @event.GetType(), publisher.GetType());

            publisher.Publish(@event);

            return ListOfEvents.EmptyList();
        }
    }
}