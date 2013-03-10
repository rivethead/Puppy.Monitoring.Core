using System;
using Puppy.Monitoring.Publishing;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Adapters
{
    public class when_publishing_an_event_and_the_pipeline_adapter_is_not_available : Specification
    {
        private Exception actual_exception;

        public override void Observe()
        {
            try
            {
                Publisher.Reset();
                new Publisher().Publish(new TestEvent());
            }
            catch (Exception e)
            {
                actual_exception = e;
            }
        }

        [Observation]
        public void an_exception_is_thrown()
        {
            actual_exception.ShouldNotBeNull();
        }

        [Observation]
        public void the_exception_explains_what_is_wrong_with_the_publisher()
        {
            actual_exception.Message.ToLowerInvariant()
                .ShouldContain("pipeline adapter is null");
            actual_exception.Message.ToLowerInvariant()
                .ShouldContain("use the publisher.use method");
        }

    }
}