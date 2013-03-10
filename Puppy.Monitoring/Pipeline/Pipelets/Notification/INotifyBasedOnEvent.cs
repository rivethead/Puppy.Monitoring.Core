using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Notification
{
    public interface INotifyBasedOnEvent
    {
        void Raise(IEvent @event);
    }
}