using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Adapters.Default
{
    internal interface IContainPipelines
    {
        void Push(IEvent @event);
    }
}