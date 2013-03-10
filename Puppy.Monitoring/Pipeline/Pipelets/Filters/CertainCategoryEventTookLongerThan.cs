using System;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Filters
{
    public class CertainCategoryEventTookLongerThan : IEventSpecification
    {
        private readonly string category;
        private readonly long longerOrEqualTo;
        private readonly IEventSpecification successor = new NullEventSpecification();

        public CertainCategoryEventTookLongerThan(string category, long longerOrEqualTo)
            : this(category, longerOrEqualTo, new NullEventSpecification())
        {
        }

        public CertainCategoryEventTookLongerThan(string category, long longerOrEqualTo, IEventSpecification successor)
        {
            this.category = category;
            this.longerOrEqualTo = longerOrEqualTo;
            this.successor = successor;
        }

        public bool SatisfiedBy(IEvent @event)
        {
            return @event.Categorisation.Category.Equals(category, StringComparison.InvariantCultureIgnoreCase) &&
                   @event.Timings.Took > longerOrEqualTo &&
                   successor.SatisfiedBy(@event);
        }
    }
}