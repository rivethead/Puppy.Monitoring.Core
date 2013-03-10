using System;
using Puppy.Monitoring.Publishing;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Publishing
{
    public class when_a_null_event_is_published : Specification
    {
        private readonly TestEvent expected_event;
        private readonly PublishedEventListener test_publisher = new PublishedEventListener();
        private Exception actual_exception;

        public when_a_null_event_is_published()
        {
            Publisher.Use(new TestPipelineAdapter(test_publisher), new PublishingContext("TEST_SYSTEM", "TEST"));
            expected_event = null;
        }

        public override void Observe()
        {
            try
            {
                new Publisher().Publish(expected_event);
            }
            catch (Exception e)
            {
                actual_exception = e;
            }
        }

        [Observation]
        public void the_publisher_provided_by_the_configuration_is_not_invoked()
        {
            test_publisher.WasInvoked.ShouldBeFalse();
        }

        [Observation]
        public void an_exception_is_thrown()
        {
            actual_exception.ShouldNotBeNull();
        }

        [Observation]
        public void an_exception_is_thrown_with_information_about_the_exception()
        {
            actual_exception.Message
                .Contains("is null")
                .ShouldBeTrue();
        }

    }
}