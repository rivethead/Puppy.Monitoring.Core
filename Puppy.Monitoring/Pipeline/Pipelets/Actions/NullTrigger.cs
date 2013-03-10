using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Actions
{
    public class NullTrigger : ITriggerActionBasedOnEvent
    {
        public void Trigger(IEvent @event)
        {
        }
    }
}