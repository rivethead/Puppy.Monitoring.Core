using System;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class ExternalServiceCalledFailureEvent : Event
    {
        public ExternalServiceCalledFailureEvent(PublishingContext context, EventTiming eventAudit, Categorisation categorisation, Guid correlationId, Timings timings, Guid id) : base(context, eventAudit, categorisation, correlationId, timings, id)
        {
        }

        public ExternalServiceCalledFailureEvent() : base(new Categorisation("unknown"), Guid.Empty)
        {
        }

        public ExternalServiceCalledFailureEvent(Categorisation categorisation, Guid correlationId) : base(categorisation, correlationId)
        {
        }

        public ExternalServiceCalledFailureEvent(Categorisation categorisation, Timings timings, Guid correlationId)
            : base(categorisation, timings, correlationId)
        {
        }

        public ExternalServiceCalledFailureEvent(DateTime publishedOn, Categorisation categorisation, Timings timings)
            : base(publishedOn, categorisation, timings)
        {
        }

        public ExternalServiceCalledFailureEvent(DateTime publishedOn, Categorisation categorisation, Timings timings, Guid correlationId)
            : base(publishedOn, categorisation, timings, correlationId)
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