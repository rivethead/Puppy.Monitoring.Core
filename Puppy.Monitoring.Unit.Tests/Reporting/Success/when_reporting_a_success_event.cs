using System;
using System.Linq;
using Puppy.Monitoring.Publishing;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Reporting.Success
{
    public class when_reporting_a_success_event : Specification
    {
        private readonly PublishedEventListener publisher = new PublishedEventListener();

        public when_reporting_a_success_event()
        {
            SystemTime.Now = () => new DateTime(2013, 04, 20, 12, 13, 14);

            Publisher.Use(new TestPipelineAdapter(publisher), new PublishingContext("TEST_SYSTEM", "TEST"));
        }

        public override void Observe()
        {
            Report.Success().Publish();
        }


        [Observation]
        public void the_categorisation_on_the_event_is_not_set()
        {
            var actual_categorisation = publisher.Events.First().Categorisation;

            string.IsNullOrEmpty(actual_categorisation.Category).ShouldBeTrue();
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