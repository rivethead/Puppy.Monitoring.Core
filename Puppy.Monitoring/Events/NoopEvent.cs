using System;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class NoopEvent : IEvent
    {
        public Guid Id { get; private set; }
        public EventTiming EventAudit { get; private set; }
        public Categorisation Categorisation { get; private set; }
        public Timings Timings { get; private set; }
        public PublishingContext Context { get; private set; }
        public void AttachContext(PublishingContext context)
        {
            Context = context;
        }

        public Guid CorrelationId { get; private set; }
    }
}