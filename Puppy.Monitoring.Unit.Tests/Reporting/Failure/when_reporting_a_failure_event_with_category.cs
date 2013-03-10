using System;
using System.Linq;
using Puppy.Monitoring.Publishing;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Reporting.Failure
{
    public class when_reporting_a_failure_event_with_category : Specification
    {
        private readonly PublishedEventListener publisher = new PublishedEventListener();
        private const string expected_category = "expected_catgory";

        public when_reporting_a_failure_event_with_category()
        {
            SystemTime.Now = () => new DateTime(2013, 04, 20, 12, 13, 14);

            Publisher.Use(new TestPipelineAdapter(publisher), new PublishingContext("TEST_SYSTEM", "TEST"));
        }

        public override void Observe()
        {
            Report
                .Failure()
                .InCategory(expected_category)
                .Publish();
        }

        [Observation]
        public void the_category_is_set_on_the_categorisation()
        {
            var actual_categorisation = publisher.Events.First().Categorisation;
            actual_categorisation.Category.ShouldEqual(expected_category);
        }

        [Observation]
        public void the_sub_category_is_empty_on_the_categorisation()
        {
            var actual_categorisation = publisher.Events.First().Categorisation;
            string.IsNullOrEmpty(actual_categorisation.SubCategory).ShouldBeTrue();
        }

        [Observation]
        public void the_audit_for_the_event_is_completed()
        {
            var actual_audit = publisher.Events.First().EventAudit;

            actual_audit.PublishedOn.ShouldEqual(SystemTime.Now());
            actual_audit.Buckets.Year.ShouldEqual(2013);
            actual_audit.Buckets.Month.ShouldEqual(4);
            actual_audit.Buckets.Day.ShouldEqual(20);
            actual_audit.Buckets.Hour.ShouldEqual(12);
            actual_audit.Buckets.Minute.ShouldEqual(13);
            actual_audit.Buckets.Second.ShouldEqual(14);
        }


        [Observation]
        public void the_timings_for_the_event_is_not_set()
        {
            var actual_timings = publisher.Events.First().Timings;

            actual_timings.Took.ShouldEqual(int.MinValue);
        }


        [Observation]
        public void only_one_event_is_published()
        {
            publisher.Events.Count().ShouldEqual(1);
        }
    }
}