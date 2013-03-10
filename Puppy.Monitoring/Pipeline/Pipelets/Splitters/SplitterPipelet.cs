using System.Collections.Generic;
using Common.Logging;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Splitters
{
    public class SplitterPipelet : IPipelet
    {
        private static readonly ILog log = LogManager.GetLogger<SplitterPipelet>();
        private readonly IMakeSplitterDecisions decisionMaker;

        public SplitterPipelet(IMakeSplitterDecisions decisionMaker)
        {
            this.decisionMaker = decisionMaker;
        }

        public IEnumerable<IEvent> Flow(IEvent @event)
        {
            log.InfoFormat("Received {0}", @event.GetType());
            log.DebugFormat("Flowing {0} to decision maker {1} to move the event forward", @event.GetType(),
                            decisionMaker.GetType());

            return decisionMaker.MakeDecision(@event);
        }

        public bool WantsEvent(IEvent @event)
        {
            return true;
        }
    }
}