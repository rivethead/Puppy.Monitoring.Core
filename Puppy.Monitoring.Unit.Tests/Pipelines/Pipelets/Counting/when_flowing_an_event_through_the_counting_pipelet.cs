using System.Collections.Generic;
using System.Linq;
using Moq;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets.Counting;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Pipelines.Pipelets.Counting
{
    public class when_flowing_an_event_through_the_counting_pipelet : Specification
    {
        private readonly EventCountingPipelet pipelet;
        private IEnumerable<IEvent> resulting_events;
        private readonly Mock<ICountEvents> counter;

        public when_flowing_an_event_through_the_counting_pipelet()
        {
            counter = new Mock<ICountEvents>();
            pipelet = new EventCountingPipelet(counter.Object);
        }

        public override void Observe()
        {
            resulting_events = pipelet.Flow(new TestEvent());
        }

        [Observation]
        public void the_pipelet_publishes_the_number_of_events()
        {
            counter.Verify(c => c.Increment(It.IsAny<IEvent>()), Times.Once());
        }

        [Observation]
        public void the_pipelet_increments_the_number_of_events()
        {
            resulting_events.Count().ShouldEqual(1);
            resulting_events.First().ShouldBeType<NumberOfEventsEvent>();
        }
    }
}