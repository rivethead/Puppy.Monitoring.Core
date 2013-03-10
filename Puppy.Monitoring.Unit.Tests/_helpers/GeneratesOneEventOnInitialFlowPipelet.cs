using System.Collections.Generic;
using System.Linq;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets;

namespace Puppy.Monitoring.Unit.Tests._helpers
{
    public class GeneratesOneEventOnInitialFlowPipelet : IPipelet
    {
        public GeneratesOneEventOnInitialFlowPipelet()
        {
            Events = new List<IEvent>();
        }

        public IList<IEvent> Events { get; private set; }

        public bool WasCalled
        {
            get { return Events.Any(); }
        }

        public int NumberOfTimesCalled
        {
            get { return Events.Count; }
        }

        public IEnumerable<IEvent> Flow(IEvent @event)
        {
            Events.Add(@event);

            if (NumberOfTimesCalled == 1)
                return new List<IEvent>
                    {
                        new AnotherTestEvent()
                    };

            return new List<IEvent>
                {
                    new NoopEvent()
                };
        }

        public bool WantsEvent(IEvent @event)
        {
            return true;
        }
    }
}