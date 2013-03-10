using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Notification
{
    public class NullNotifier : INotifyBasedOnEvent
    {
        public void Raise(IEvent @event)
        {
        }
    }
}