using Common.Logging;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Actions
{
    public class WriteToLoggingActionTrigger : ITriggerActionBasedOnEvent
    {
        private static readonly ILog log = LogManager.GetLogger<WriteToLoggingActionTrigger>();

        public void Trigger(IEvent @event)
        {
            log.DebugFormat(@event.ToString());
        }
    }
}