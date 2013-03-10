using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Filters
{
    public interface IEventSpecification
    {
        bool SatisfiedBy(IEvent @event);
    }

    public class NullEventSpecification : IEventSpecification
    {
        public bool SatisfiedBy(IEvent @event)
        {
            return true;
        }
    }
}