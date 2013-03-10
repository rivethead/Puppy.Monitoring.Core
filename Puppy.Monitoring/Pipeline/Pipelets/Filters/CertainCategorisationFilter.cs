using System;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Filters
{
    public class CertainCategorisationFilter : IEventSpecification
    {
        private readonly string category;
        private readonly string subCategory;
        private readonly IEventSpecification successor = new NullEventSpecification();

        public CertainCategorisationFilter(string category, string subCategory, IEventSpecification successor)
        {
            this.category = category;
            this.subCategory = subCategory;
            this.successor = successor;
        }

        public CertainCategorisationFilter(string category, string subCategory)
        {
            this.category = category;
            this.subCategory = subCategory;
        }

        public bool SatisfiedBy(IEvent @event)
        {
            return category.Equals(@event.Categorisation.Category, StringComparison.InvariantCultureIgnoreCase) &&
                   subCategory.Equals(@event.Categorisation.SubCategory, StringComparison.InvariantCultureIgnoreCase) && 
                   successor.SatisfiedBy(@event);
        }
    }
}