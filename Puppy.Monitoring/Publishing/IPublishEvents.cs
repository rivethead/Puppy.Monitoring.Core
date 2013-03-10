using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Publishing
{
    public interface IPublishEvents
    {
        void Publish(IEvent @event);
    }

    public interface IPublishEvents<in T> : IPublishEvents where T : IEvent
    {
        void Publish(T @event);
    }
}