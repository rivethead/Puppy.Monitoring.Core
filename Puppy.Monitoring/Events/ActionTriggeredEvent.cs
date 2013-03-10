using System;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Events
{
    [Serializable]
    public class ActionTriggeredEvent : Event, IAdminEvent
    {
        public ActionTriggeredEvent(Categorisation categorisation, Guid correlationId) : base(categorisation, correlationId)
        {
        }

        public ActionTriggeredEvent(IEvent @event)
            : base(SystemTime.Now(), 
                    new Categorisation(string.Format("Triggers/{0}", @event.Categorisation.Category), @event.GetType().FullName), 
                    new Timings(0))
        {
        }

        public ActionTriggeredEvent(Categorisation categorisation, Timings timings, Guid correlationId) : base(categorisation, timings, correlationId)
        {
        }

        public ActionTriggeredEvent(DateTime publishedOn, Categorisation categorisation, Timings timings) : base(publishedOn, categorisation, timings)
        {
        }

        public ActionTriggeredEvent(PublishingContext context, EventTiming eventAudit, Categorisation categorisation, Guid correlationId, Timings timings, Guid id) : base(context, eventAudit, categorisation, correlationId, timings, id)
        {
        }

        public ActionTriggeredEvent(DateTime publishedOn, Categorisation categorisation, Timings timings, Guid correlationId) : base(publishedOn, categorisation, timings, correlationId)
        {
        }
    }
}