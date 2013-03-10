using System;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Notification
{
    public class ConsoleNotifier : INotifyBasedOnEvent
    {
        public void Raise(IEvent @event)
        {
            Console.WriteLine(@event);
        }
    }
}