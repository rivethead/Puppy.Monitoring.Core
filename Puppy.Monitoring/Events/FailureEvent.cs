using System;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class FailureEvent : Event
    {
        public FailureEvent(PublishingContext context, EventTiming eventAudit, Categorisation categorisation, Guid correlationId, Timings timings, Guid id) : base(context, eventAudit, categorisation, correlationId, timings, id)
        {
        }

        public FailureEvent(Categorisation categorisation, Timings timings)
            : base(SystemTime.Now(), categorisation, timings)
        {
        }

        public FailureEvent(Categorisation categorisation, Timings timings, Guid correlationId) : base(categorisation, timings, correlationId)
        {
        }

        public FailureEvent(Categorisation categorisation)
            : base(SystemTime.Now(), categorisation, new Timings(int.MinValue))
        {
        }

        public FailureEvent(Categorisation categorisation, Guid correlationId) : base(categorisation, new Timings(int.MinValue), correlationId)
        {
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