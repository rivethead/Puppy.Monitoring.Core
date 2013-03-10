using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline;

namespace Puppy.Monitoring.Adapters
{
    public interface IPipelineAdapter
    {
        void Push(IEvent @event);
        IPipelineAdapter Register(IPipeline pipeline);
    }
}