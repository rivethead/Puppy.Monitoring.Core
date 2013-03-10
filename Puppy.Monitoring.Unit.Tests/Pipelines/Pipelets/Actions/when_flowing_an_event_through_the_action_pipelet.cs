using System.Collections.Generic;
using System.Linq;
using Moq;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets.Actions;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Pipelines.Pipelets.Actions
{
    public class when_flowing_an_event_through_the_action_pipelet : Specification
    {
        private readonly TriggerActionPipelet pipelet;
        private readonly Mock<ITriggerActionBasedOnEvent> trigger;
        private IEnumerable<IEvent> resulting_events;

        public when_flowing_an_event_through_the_action_pipelet()
        {
            trigger = new Mock<ITriggerActionBasedOnEvent>();

            pipelet = new TriggerActionPipelet(trigger.Object, new TestEventSpecification());
        }

        public override void Observe()
        {
            resulting_events = pipelet.Flow(new TestEvent());
        }

        [Observation]
        public void the_trigger_is_pulled()
        {
            trigger.Verify(t => t.Trigger(It.IsAny<IEvent>()), Times.Once());
        }

        [Observation]
        public void the_fact_that_the_trigger_is_pulled_is_available_to_the_pipeline()
        {
            resulting_events.Count().ShouldEqual(1);
            resulting_events.First().ShouldBeType<ActionTriggeredEvent>();
        }


    }
}