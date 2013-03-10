using System.Collections.Generic;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Splitters
{
    public interface IMakeSplitterDecisions
    {
        IEnumerable<IEvent> MakeDecision(IEvent @event);
    }
}