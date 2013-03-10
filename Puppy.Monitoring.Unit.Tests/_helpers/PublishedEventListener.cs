using System.Collections.Generic;
using System.Linq;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Unit.Tests._helpers
{
    public class PublishedEventListener : IPublishEvents
    {
        private readonly IList<IEvent> events = new List<IEvent>();

        public IEnumerable<IEvent> Events
        {
            get { return events; }
        }

        public bool WasInvoked
        {
            get { return events.Any(); }
        }

        public int NumberOfTimesInvoked
        {
            get { return events.Count(); }
        }

        public void Publish(IEvent @event)
        {
            events.Add(@event);
        }
    }
}