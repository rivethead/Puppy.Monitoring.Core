using System;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Events
{
    public class NullReportEvent : IEvent
    {
        public Guid Id { get; private set; }

        public EventTiming EventAudit
        {
            get { return null; }
        }

        public Categorisation Categorisation
        {
            get { return null; }
        }

        public Timings Timings
        {
            get { return null; }
        }

        public PublishingContext Context { get; private set; }
        public void AttachContext(PublishingContext context)
        {
            Context = context;
        }

        public Guid CorrelationId { get; private set; }
    }
}