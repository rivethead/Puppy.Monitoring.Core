using Puppy.Monitoring.Adapters;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Unit.Tests._helpers
{
    public class TestPipelineAdapter : IPipelineAdapter
    {
        private readonly IPublishEvents publisher;

        public TestPipelineAdapter() : this(new PublishedEventListener())
        {
        }

        public TestPipelineAdapter(IPublishEvents publisher)
        {
            this.publisher = publisher;
        }

        public void Push(IEvent @event)
        {
            publisher.Publish(@event);
        }

        public IPipelineAdapter Register(IPipeline pipeline)
        {
            return this;
        }
    }
}