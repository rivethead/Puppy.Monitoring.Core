using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Filters
{
    public class SuccessEventSpecification : IEventSpecification
    {
        private readonly IEventSpecification successor = new NullEventSpecification();

        public SuccessEventSpecification()
        {
        }

        public SuccessEventSpecification(IEventSpecification successor)
        {
            this.successor = successor;
        }

        public bool SatisfiedBy(IEvent @event)
        {
            return @event.GetType() == typeof (SuccessEvent) && successor.SatisfiedBy(@event);
        }
    }
}