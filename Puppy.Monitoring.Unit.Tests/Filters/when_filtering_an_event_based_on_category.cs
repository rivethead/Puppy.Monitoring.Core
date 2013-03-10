using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets.Filters;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Filters
{
    public class when_filtering_an_event_based_on_category : Specification
    {
        private readonly CertainCategoryFilter filter;
        private readonly string category;
        private bool satisfied;

        public when_filtering_an_event_based_on_category()
        {
            category = "category";
            filter = new CertainCategoryFilter(category);
        }

        public override void Observe()
        {
            satisfied = filter.SatisfiedBy(new TestEvent(new Categorisation(category, string.Empty)));
        }

        [Observation]
        public void the_filter_is_satisfied_by_the_category()
        {
            satisfied.ShouldBeTrue();
        }
    }
}