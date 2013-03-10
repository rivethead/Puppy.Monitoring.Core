using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets.Filters;

namespace Puppy.Monitoring.Unit.Tests._helpers
{
    public class TestEventSpecification : IEventSpecification
    {
        public bool SatisfiedBy(IEvent @event)
        {
            return @event.GetType() == typeof (TestEvent);
        }
    }
}