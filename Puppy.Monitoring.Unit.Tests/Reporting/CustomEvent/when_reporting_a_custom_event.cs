using System;
using System.Linq;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Reporting.CustomEvent
{
    public class when_reporting_a_custom_event : Specification
    {
        private readonly PublishedEventListener publisher = new PublishedEventListener();
        private readonly Guid correlation_id = Guid.NewGuid();
        private const long time_it_took = 12345;
        private const string segment = "SEGMENT";
        private const string sub_category = "SUB_CATEGORY";
        private const string category = "CATEGORY";

        public when_reporting_a_custom_event()
        {
            SystemTime.Now = () => new DateTime(2013, 04, 20, 12, 13, 14);

            Publisher.Use(new TestPipelineAdapter(publisher), new PublishingContext("TEST_SYSTEM", "TEST"));
        }

        private IEvent Event
        {
            get { return publisher.Events.First(); }
        }

        public override void Observe()
        {
            Report
                .CustomEvent()
                .Event(new TestEvent())
                    .InCategory(category)
                    .InSubCategory(sub_category)
                    .SegmentedAs(segment)
                    .RelateTo(correlation_id)
                    .ItTook(time_it_took)
                .Publish();
        }

        [Observation]
        public void the_custom_event_is_published()
        {
            Event.ShouldBeType<TestEvent>();
        }

        [Observation]
        public void the_categorisation_is_set()
        {
            Event.Categorisation.Category.ShouldEqual(category);
            Event.Categorisation.SubCategory.ShouldEqual(sub_category);
            Event.Categorisation.Segmentation.ShouldEqual(segment);
        }

        [Observation]
        public void the_correlation_id_is_set()
        {
            Event.CorrelationId.ShouldEqual(correlation_id);
        }

        [Observation]
        public void the_time_the_event_took_is_recorded()
        {
            Event.Timings.Took.ShouldEqual(time_it_took);
        }



    }
}