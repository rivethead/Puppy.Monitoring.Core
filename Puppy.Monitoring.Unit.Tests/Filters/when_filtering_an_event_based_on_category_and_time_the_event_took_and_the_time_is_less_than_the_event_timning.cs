using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets.Filters;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Filters
{
    public class when_filtering_an_event_based_on_category_and_time_the_event_took_and_the_time_is_less_than_the_event_timning : Specification
    {
        private readonly CertainCategoryEventTookLongerThan filter;
        private bool satisfied;
        private readonly string category;
        private readonly long time_limit;

        public when_filtering_an_event_based_on_category_and_time_the_event_took_and_the_time_is_less_than_the_event_timning()
        {
            category = "Category";
            time_limit = 500;

            filter = new CertainCategoryEventTookLongerThan(category, time_limit);
        }

        public override void Observe()
        {
            satisfied = filter.SatisfiedBy(new TestEvent(new Categorisation(category), new Timings(250)));
        }

        [Observation]
        public void the_filter_is_not_satisfied()
        {
            satisfied.ShouldBeFalse();
        }
    }
}