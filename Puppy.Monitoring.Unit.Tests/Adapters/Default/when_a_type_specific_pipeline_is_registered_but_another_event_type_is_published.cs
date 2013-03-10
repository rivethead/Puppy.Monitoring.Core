using Puppy.Monitoring.Adapters;
using Puppy.Monitoring.Adapters.Default;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Adapters.Default
{
    public class when_a_type_specific_pipeline_is_registered_but_another_event_type_is_published : Specification
    {
        private readonly IPipelineAdapter adapter;
        private readonly EventSpecificPipeline specific_publisher;

        public when_a_type_specific_pipeline_is_registered_but_another_event_type_is_published()
        {
            specific_publisher = new EventSpecificPipeline();

            adapter = new ManualPipelineAdapter()
                .Register(specific_publisher);
        }

        public override void Observe()
        {
            adapter.Push(new AnotherTestEvent());
        }

        [Observation]
        public void the_type_specific_pipeline_is_not_invoked()
        {
            specific_publisher.WasCalled.ShouldBeFalse();
        }


    }
}