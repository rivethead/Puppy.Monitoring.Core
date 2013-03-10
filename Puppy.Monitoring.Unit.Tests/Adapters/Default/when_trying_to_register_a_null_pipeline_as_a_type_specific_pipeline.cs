using System;
using Puppy.Monitoring.Adapters.Default;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Adapters.Default
{
    public class when_trying_to_register_a_null_pipeline_as_a_type_specific_pipeline : Specification
    {
        private readonly EventSpecificPipeline specific_publisher;
        private Exception actual_exception;

        public when_trying_to_register_a_null_pipeline_as_a_type_specific_pipeline()
        {
            specific_publisher = null;
        }

        public override void Observe()
        {
            try
            {
                new ManualPipelineAdapter()
                    .Register(specific_publisher);
            }
            catch (Exception e)
            {
                actual_exception = e;
            }
        }

        [Observation]
        public void an_exception_is_thrown_indicating_an_invalid_publisher_is_registered()
        {
            actual_exception.ShouldNotBeNull();
            actual_exception.Message.ToLowerInvariant().Contains("invalid pipeline")
                            .ShouldBeTrue();
        }
    }
}