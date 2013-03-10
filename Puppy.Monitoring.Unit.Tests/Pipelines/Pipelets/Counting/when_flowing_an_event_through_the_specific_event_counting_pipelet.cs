using System.Collections.Generic;
using System.Linq;
using Moq;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets.Counting;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Pipelines.Pipelets.Counting
{
    public class when_flowing_an_event_through_the_specific_event_counting_pipelet : Specification
    {
        private readonly Mock<ICountEvents> counter;
        private readonly SpecificEventCountingPipelet pipelet;
        private readonly List<IEvent> resulting_events;

        public when_flowing_an_event_through_the_specific_event_counting_pipelet()
        {
            resulting_events = new List<IEvent>();
            counter = new Mock<ICountEvents>();
            pipelet = new SpecificEventCountingPipelet(counter.Object, new TestEventSpecification());
        }

        public override void Observe()
        {
            resulting_events.AddRange(pipelet.Flow(new TestEvent()));
            resulting_events.AddRange(pipelet.Flow(new AnotherTestEvent()));
        }

        [Observation]
        public void the_pipelet_publishes_the_number_of_events()
        {
            counter.Verify(c => c.Increment(It.IsAny<IEvent>()), Times.Once());
        }

        [Observation]
        public void the_pipelet_increments_the_number_of_events()
        {
            resulting_events.Count().ShouldEqual(2); // counter + noop
            resulting_events.First().ShouldBeType<NumberOfSpecificEventEvent>();
        }
    }
}