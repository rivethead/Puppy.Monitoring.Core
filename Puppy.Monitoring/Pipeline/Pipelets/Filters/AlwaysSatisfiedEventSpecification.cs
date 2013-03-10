using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Filters
{
    public class AlwaysSatisfiedEventSpecification : IEventSpecification
    {
        private readonly IEventSpecification successor = new NullEventSpecification();

        public AlwaysSatisfiedEventSpecification()
        {
        }

        public AlwaysSatisfiedEventSpecification(IEventSpecification successor)
        {
            this.successor = successor;
        }

        public bool SatisfiedBy(IEvent @event)
        {
            return true && successor.SatisfiedBy(@event);
        }
    }
}