using System;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Events
{
    public interface IEvent
    {
        Guid Id { get; }
        EventTiming EventAudit { get; }
        Categorisation Categorisation { get; }
        Timings Timings { get; }
        PublishingContext Context { get; }
        void AttachContext(PublishingContext context);
        Guid CorrelationId { get; }
    }
}