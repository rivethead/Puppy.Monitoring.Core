using System;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Events
{
    public class NumberOfSpecificEventEvent : Event, IAdminEvent
    {
        public NumberOfSpecificEventEvent(PublishingContext context, EventTiming eventAudit, Categorisation categorisation, Guid correlationId, Timings timings, Guid id) : base(context, eventAudit, categorisation, correlationId, timings, id)
        {
        }

        public NumberOfSpecificEventEvent(int number, IEvent @event)
            : base(SystemTime.Now(), new Categorisation("NumberOfEvents", @event.GetType().FullName), new Timings(0))
        {
            Number = number;
        }

        public int Number { get; private set; }
    }
}