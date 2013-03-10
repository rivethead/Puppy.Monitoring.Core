using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Publishing
{
    public class NullPublisher : IPublishEvents<IEvent>
    {
        public void Publish(IEvent @event)
        {
            
        }
    }
}