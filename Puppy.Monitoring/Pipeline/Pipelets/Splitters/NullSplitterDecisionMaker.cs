using System.Collections.Generic;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Splitters
{
    public class NullSplitterDecisionMaker : IMakeSplitterDecisions
    {
        public IEnumerable<IEvent> MakeDecision(IEvent @event)
        {
            return ListOfEvents.EmptyList();
        }
    }
}