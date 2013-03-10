using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Filters
{
    public class FailureEventSpecification : IEventSpecification
    {
        private readonly IEventSpecification successor = new NullEventSpecification();

        public FailureEventSpecification()
        {
        }

        public FailureEventSpecification(IEventSpecification successor)
        {
            this.successor = successor;
        }

        public bool SatisfiedBy(IEvent @event)
        {
            return @event.GetType() == typeof (FailureEvent) && successor.SatisfiedBy(@event);
        }
    }
}