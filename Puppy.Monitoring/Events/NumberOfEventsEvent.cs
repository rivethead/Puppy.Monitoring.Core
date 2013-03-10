using System;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class NumberOfEventsEvent : Event, IAdminEvent
    {
        public NumberOfEventsEvent(PublishingContext context, EventTiming eventAudit, Categorisation categorisation, Guid correlationId, Timings timings, Guid id) : base(context, eventAudit, categorisation, correlationId, timings, id)
        {
        }

        public NumberOfEventsEvent(int number)
            : base(SystemTime.Now(), new Categorisation("NumberOfEvents", "All"), new Timings(0))
        {
            Number = number;
        }

        public int Number { get; private set; }
    }
}