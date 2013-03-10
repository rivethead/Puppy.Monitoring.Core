using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets;

namespace Puppy.Monitoring.Pipeline
{
    public class NullPipeline : IPipeline
    {
        private static readonly ILog log = LogManager.GetLogger<NullPipeline>();

        public void Flow(IEvent @event)
        {
            log.DebugFormat("In the null pipeline. Events come here to die, including {0}", @event.GetType());
        }

        public IPipeline Add(IPipelet pipelet)
        {
            return this;
        }
    }
}