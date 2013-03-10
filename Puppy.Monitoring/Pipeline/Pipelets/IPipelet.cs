using System.Collections.Generic;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets
{
    public interface IPipelet
    {
        IEnumerable<IEvent> Flow(IEvent @event);
        bool WantsEvent(IEvent @event);
    }
}