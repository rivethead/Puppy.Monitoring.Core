using System.Collections.Generic;
using System.Linq;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets;

namespace Puppy.Monitoring.Unit.Tests._helpers
{
    public class SamplePipelet : IPipelet
    {
        public SamplePipelet()
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