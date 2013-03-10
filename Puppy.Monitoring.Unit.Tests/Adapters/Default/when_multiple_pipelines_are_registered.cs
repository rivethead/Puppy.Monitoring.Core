using Puppy.Monitoring.Adapters;
using Puppy.Monitoring.Adapters.Default;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Adapters.Default
{
    public class when_multiple_pipelines_are_registered : Specification
    {
        private readonly IPipelineAdapter adapter;
        private readonly GenericPipeline generic_publisher_1;
        private readonly EventSpecificPipeline specific_publisher_1;
        private readonly GenericPipeline generic_publisher_2;
        private readonly EventSpecificPipeline specific_publisher_2;

        public when_multiple_pipelines_are_registered()
        {
            generic_publisher_1 = new GenericPipeline();
            generic_publisher_2 = new GenericPipeline();
            specific_publisher_1 = new EventSpecificPipeline();
            specific_publisher_2 = new EventSpecificPipeline();

            adapter = new ManualPipelineAdapter()
                .Register(generic_publisher_1)
                .Register(specific_publisher_1)
                .Register(generic_publisher_2)
                .Register(specific_publisher_2);
        }

        public override void Observe()
        {
            adapter.Push(new TestEvent());
        }

        [Observation]
        public void all_the_generic_pipeline_are_invoked()
        {
            generic_publisher_1.WasCalled.ShouldBeTrue();
            generic_publisher_2.WasCalled.ShouldBeTrue();
        }

        [Observation]
        public void all_the_type_specific_pipeline_are_invoked()
        {
            specific_publisher_1.WasCalled.ShouldBeTrue();
            specific_publisher_2.WasCalled.ShouldBeTrue();
        }
    }
}