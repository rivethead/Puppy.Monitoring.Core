using System.Collections.Generic;
using System.Linq;
using Puppy.Monitoring.Pipeline;
using Puppy.Monitoring.Pipeline.Pipelets;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Pipelines
{
    public class when_flowing_an_event_through_a_queued_pipeline : Specification
    {
        private readonly IPipeline pipe_line;
        private readonly SamplePipelet pipelet_1;
        private readonly SamplePipelet pipelet_2;
        private readonly GeneratesOneEventOnInitialFlowPipelet pipelet_3;
        private readonly SamplePipelet pipelet_4;

        public when_flowing_an_event_through_a_queued_pipeline()
        {
            pipelet_1 = new SamplePipelet();
            pipelet_2 = new SamplePipelet();
            pipelet_3 = new GeneratesOneEventOnInitialFlowPipelet();
            pipelet_4 = new SamplePipelet();

            pipe_line = new QueuedEventsPipeline(new List<IPipelet>
                {
                    pipelet_1,
                    pipelet_2,
                    pipelet_3,
                    pipelet_4,
                });
        }

        public override void Observe()
        {
            pipe_line.Flow(new TestEvent());
        }

        [Observation]
        public void each_pipelet_is_invoked()
        {
            pipelet_1.WasCalled.ShouldBeTrue();
            pipelet_2.WasCalled.ShouldBeTrue();
            pipelet_3.WasCalled.ShouldBeTrue();
            pipelet_4.WasCalled.ShouldBeTrue();
        }

        [Observation]
        public void the_pipelets_preceeding_the_generating_pipelet_is_called_twice()
        {
            pipelet_1.NumberOfTimesCalled.ShouldEqual(2);
            pipelet_2.NumberOfTimesCalled.ShouldEqual(2);
        }

        [Observation]
        public void the_pipelets_preceeding_the_generating_pipelet_get_the_events_in_the_correct_order()
        {
            pipelet_1.Events.First().ShouldBeType<TestEvent>();
            pipelet_1.Events.Last().ShouldBeType<AnotherTestEvent>();

            pipelet_2.Events.First().ShouldBeType<TestEvent>();
            pipelet_2.Events.Last().ShouldBeType<AnotherTestEvent>();
        }

        [Observation]
        public void the_generating_pipelet_is_called_twice()
        {
            pipelet_3.NumberOfTimesCalled.ShouldEqual(2);
        }

        [Observation]
        public void the_generating_pipelet_get_the_events_in_the_correct_order()
        {
            pipelet_3.Events.First().ShouldBeType<TestEvent>();
            pipelet_3.Events.Last().ShouldBeType<AnotherTestEvent>();
        }

        [Observation]
        public void the_pipelets_following_the_generating_pipelet_is_called_twice()
        {
            pipelet_4.NumberOfTimesCalled.ShouldEqual(2);
        }

        [Observation]
        public void the_pipelets_following_the_generating_pipelet_receives_the_generated_event_before_the_original_event()
        {
            pipelet_4.Events.First().ShouldBeType<TestEvent>();
            pipelet_4.Events.Last().ShouldBeType<AnotherTestEvent>();
        }
    }
}