using Puppy.Monitoring.Adapters;
using Puppy.Monitoring.Adapters.Default;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Adapters.Default
{
    public class when_a_type_agnostic_pipeline_and_type_specific_pipeline_is_registered : Specification
    {
        private readonly IPipelineAdapter adapter;
        private readonly GenericPipeline generic_pipeline;
        private readonly EventSpecificPipeline specific_pipeline;

        public when_a_type_agnostic_pipeline_and_type_specific_pipeline_is_registered()
        {
            generic_pipeline = new GenericPipeline();
            specific_pipeline = new EventSpecificPipeline();

            adapter = new ManualPipelineAdapter()
                .Register(generic_pipeline)
                .Register(specific_pipeline);
        }

        public override void Observe()
        {
            adapter.Push(new TestEvent());
        }

        [Observation]
        public void the_generic_pipeline_is_invoked()
        {
            generic_pipeline.WasCalled.ShouldBeTrue();
        }

        [Observation]
        public void the_type_specific_pipeline_is_invoked()
        {
            specific_pipeline.WasCalled.ShouldBeTrue();
        }
    }
}
