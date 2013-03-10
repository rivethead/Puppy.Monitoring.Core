using System;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Publishing.Default
{
    public class ConsolePublisher : IPublishEvents
    {
        public void Publish(IEvent @event)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("Publishing event {0}", @event.GetType());
            Console.WriteLine("Event {0}", @event);
            Console.WriteLine("========================================");
        }
    }
}