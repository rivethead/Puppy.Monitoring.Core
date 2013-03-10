using Puppy.Monitoring.Publishing;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Publishing
{
    public class when_publishing_an_event : Specification
    {
        private readonly TestEvent expected_event;
        private readonly PublishedEventListener test_publisher = new PublishedEventListener();

        public when_publishing_an_event()
        {
            Publisher.Use(new TestPipelineAdapter(test_publisher), new PublishingContext("TEST_SYSTEM", "TEST"));
            expected_event = new TestEvent();
        }

        public override void Observe()
        {
            new Publisher().Publish(expected_event);
        }

        [Observation]
        public void the_publisher_provided_by_the_configuration_is_invoked()
        {
            test_publisher.WasInvoked.ShouldBeTrue();
        }


        [Observation]
        public void the_context_is_not_null()
        {
            expected_event.Context.ShouldNotBeNull();
        }


        [Observation]
        public void the_publisher_is_invoked_once()
        {
            test_publisher.NumberOfTimesInvoked.ShouldEqual(1);
        }
    }
}