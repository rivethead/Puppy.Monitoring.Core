using System;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Events
{
    public class NotificationRaisedEvent : Event, IAdminEvent
    {
        public NotificationRaisedEvent(PublishingContext context, EventTiming eventAudit, Categorisation categorisation, Guid correlationId, Timings timings, Guid id) : base(context, eventAudit, categorisation, correlationId, timings, id)
        {
        }

        public NotificationRaisedEvent(IEvent @event) :
            base(SystemTime.Now(),
                 new Categorisation(string.Format("Notification/{0}", @event.Categorisation.Category), @event.GetType().FullName),
                 new Timings(0))
        {
        }
    }
}