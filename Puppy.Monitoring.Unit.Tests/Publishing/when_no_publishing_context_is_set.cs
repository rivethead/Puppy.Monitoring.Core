using System;
using Puppy.Monitoring.Publishing;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Publishing
{
    public class when_no_publishing_context_is_set : Specification
    {
        private Exception actual_exception;

        public override void Observe()
        {
            try
            {
                new Publisher().Publish(new TestEvent());
            }
            catch (Exception e)
            {
                actual_exception = e;
            }
        }

        [Observation]
        public void an_exception_is_not_thrown()
        {
            actual_exception.ShouldBeNull();
        }
    }
}