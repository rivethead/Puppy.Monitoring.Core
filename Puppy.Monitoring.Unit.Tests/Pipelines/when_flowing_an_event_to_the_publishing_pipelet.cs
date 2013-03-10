using Moq;
using Puppy.Monitoring.Pipeline.Pipelets.Filters;
using Puppy.Monitoring.Pipeline.Pipelets.Publishing;
using Puppy.Monitoring.Publishing;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Pipelines
{
    public class when_flowing_an_event_to_the_publishing_pipelet : Specification
    {
        private readonly Mock<IPublishEvents> publisher;
        private readonly PublishingPipelet publisher_pipelet;

        public when_flowing_an_event_to_the_publishing_pipelet()
        {
            publisher = new Mock<IPublishEvents>();
            publisher_pipelet = new PublishingPipelet(publisher.Object, new NullEventSpecification());
        }

        public override void Observe()
        {
            publisher_pipelet.Flow(new TestEvent());
        }

        [Observation]
        public void the_event_is_published_using_the_publisher()
        {
            publisher.Verify(a => a.Publish(It.IsAny<TestEvent>()), Times.Once());
        }
    }
}