using System;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public abstract class Event : IEvent
    {
        protected Event(Categorisation categorisation, Guid correlationId)
            : this(SystemTime.Now(), categorisation, null, correlationId)
        {
        }

        protected Event(Categorisation categorisation, Timings timings, Guid correlationId)
            : this(SystemTime.Now(), categorisation, timings, correlationId)
        {
        }

        protected Event(DateTime publishedOn, Categorisation categorisation, Timings timings) : this(publishedOn, categorisation, timings, Guid.Empty)
        {
        }

        protected Event(PublishingContext context, EventTiming eventAudit, Categorisation categorisation, Guid correlationId, Timings timings, Guid id)
        {
            Context = context;
            EventAudit = eventAudit;
            Categorisation = categorisation;
            CorrelationId = correlationId;
            Timings = timings;
            Id = id;
        }

        protected Event(DateTime publishedOn, Categorisation categorisation, Timings timings, Guid correlationId)
        {
            Categorisation = categorisation;
            Timings = timings;
            CorrelationId = correlationId;
            EventAudit = new EventTiming(publishedOn);
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public PublishingContext Context { get; protected set; }
        public EventTiming EventAudit { get; private set; }
        public Categorisation Categorisation { get; internal set; }
        public Guid CorrelationId { get; internal set; }
        public Timings Timings { get; internal set; }

        public void AttachContext(PublishingContext context)
        {
            this.Context = context;
        }

        public override string ToString()
        {
            return string.Format("{0}{3}{1}{3}{2}",
                                 Categorisation,
                                 EventAudit,
                                 Timings,
                                 Environment.NewLine);
        }
    }
}