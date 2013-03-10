using System;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Filters
{
    public class NotSpecificEventTypeSpecification : IEventSpecification
    {
        private readonly Type notType;
        private readonly IEventSpecification successor = new NullEventSpecification();

        public NotSpecificEventTypeSpecification(Type notType)
        {
            this.notType = notType;
        }

        public NotSpecificEventTypeSpecification(Type notType, IEventSpecification successor)
        {
            this.notType = notType;
            this.successor = successor;
        }

        public bool SatisfiedBy(IEvent @event)
        {
            return @event.GetType() != notType &&
                successor.SatisfiedBy(@event);
        }
    }
}