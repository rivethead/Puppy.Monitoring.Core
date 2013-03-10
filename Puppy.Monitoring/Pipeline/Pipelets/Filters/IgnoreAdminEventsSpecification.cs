using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Filters
{
    public class IgnoreAdminEventsSpecification : IEventSpecification
    {
        private readonly IEventSpecification successor = new NullEventSpecification();

        public IgnoreAdminEventsSpecification()
        {
        }

        public IgnoreAdminEventsSpecification(IEventSpecification successor)
        {
            this.successor = successor;
        }

        public bool SatisfiedBy(IEvent @event)
        {
            return !(@event is IAdminEvent) && successor.SatisfiedBy(@event);
        }
    }
}