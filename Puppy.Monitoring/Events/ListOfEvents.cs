using System.Collections.Generic;

namespace Puppy.Monitoring.Events
{
    public class ListOfEvents
    {
        public static IList<IEvent> EmptyList()
        {
            return new List<IEvent>
                {
                    new NoopEvent()
                };
        }

        public static IEnumerable<IEvent> Create(IEvent @event)
        {
            return new List<IEvent>
                {
                    @event
                };
        }
    }
}