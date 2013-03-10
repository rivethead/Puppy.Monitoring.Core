using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Filters
{
    public class NumberOfEventsGreaterOrEqualThanSpecification : IEventSpecification
    {
        private readonly int greaterThan;
        private readonly IEventSpecification successor = new NullEventSpecification();

        public NumberOfEventsGreaterOrEqualThanSpecification(int greaterThan)
        {
            this.greaterThan = greaterThan;
        }

        public NumberOfEventsGreaterOrEqualThanSpecification(int greaterThan, IEventSpecification successor)
        {
            this.greaterThan = greaterThan;
            this.successor = successor;
        }

        public bool SatisfiedBy(IEvent @event)
        {
            if (@event.GetType() != typeof (NumberOfEventsEvent))
                return false;

            return (@event as NumberOfEventsEvent).Number >= greaterThan && successor.SatisfiedBy(@event);
        }
    }
}