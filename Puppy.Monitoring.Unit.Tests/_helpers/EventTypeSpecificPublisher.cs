using System.Collections.Generic;
using System.Linq;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Unit.Tests._helpers
{
    public class EventTypeSpecificPublisher : IPublishEvents<TestEvent>
    {
        public EventTypeSpecificPublisher()
        {
            Events = new List<TestEvent>();
        }

        public bool WasCalled
        {
            get { return Events.Any(); }
        }

        public IList<TestEvent> Events { get; private set; }

        public void Publish(TestEvent @event)
        {
            Events.Add(@event);
        }

        public void Publish(IEvent @event)
        {
            Publish(@event as TestEvent);
        }
    }

    public class GenericPublisher : IPublishEvents
    {
        public GenericPublisher()
        {
            Events = new List<IEvent>();
        }

        public bool WasCalled
        {
            get { return Events.Any(); }
        }

        public IList<IEvent> Events { get; private set; }


        public void Publish(IEvent @event)
        {
            Events.Add(@event);
        }
    }
}