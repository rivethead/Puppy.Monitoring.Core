using System.Collections.Generic;
using Puppy.Monitoring.Pipeline;
using Puppy.Monitoring.Pipeline.Pipelets;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Pipelines
{
    public class when_flowing_an_event_through_a_linear_pipe : Specification
    {
        private readonly IPipeline pipe_line;
        private readonly SamplePipelet pipelet_1;
        private readonly SamplePipelet pipelet_2;
        private readonly SamplePipelet pipelet_3;
        private readonly GeneratesOneEventOnInitialFlowPipelet pipelet_4;

        public when_flowing_an_event_through_a_linear_pipe()
        {
            pipelet_1 = new SamplePipelet();
            pipelet_2 = new SamplePipelet();
            pipelet_3 = new SamplePipelet();
            pipelet_4 = new GeneratesOneEventOnInitialFlowPipelet();

            pipe_line = new LinearPipeline(new List<IPipelet>
                {
                    pipelet_1,
                    pipelet_2,
                    pipelet_3,
                    pipelet_4
                });
        }

        public override void Observe()
        {
            pipe_line.Flow(new TestEvent());
        }

        [Observation]
        public void each_pipelet_is_invoked_once()
        {
            pipelet_1.WasCalled.ShouldBeTrue();
            pipelet_1.NumberOfTimesCalled.ShouldEqual(1);

            pipelet_2.WasCalled.ShouldBeTrue();
            pipelet_2.NumberOfTimesCalled.ShouldEqual(1);

            pipelet_3.WasCalled.ShouldBeTrue();
            pipelet_3.NumberOfTimesCalled.ShouldEqual(1);

            pipelet_4.WasCalled.ShouldBeTrue();
            pipelet_4.NumberOfTimesCalled.ShouldEqual(1);
        }
    }
}