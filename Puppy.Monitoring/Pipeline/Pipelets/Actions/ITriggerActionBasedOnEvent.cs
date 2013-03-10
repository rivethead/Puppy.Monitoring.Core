using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Actions
{
    public interface ITriggerActionBasedOnEvent
    {
        void Trigger(IEvent @event);
    }
}