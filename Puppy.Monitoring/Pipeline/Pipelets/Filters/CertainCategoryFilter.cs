using System;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Filters
{
    public class CertainCategoryFilter : IEventSpecification
    {
        private readonly string category;
        private readonly IEventSpecification successor = new NullEventSpecification();

        public CertainCategoryFilter(string category, IEventSpecification successor)
        {
            this.category = category;
            this.successor = successor;
        }

        public CertainCategoryFilter(string category)
        {
            this.category = category;
        }

        public bool SatisfiedBy(IEvent @event)
        {
            return category.Equals(@event.Categorisation.Category, StringComparison.InvariantCultureIgnoreCase) &&
                successor.SatisfiedBy(@event);
        }
    }
}