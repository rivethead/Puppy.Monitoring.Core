using System.Collections.Generic;
using System.Linq;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline;
using Puppy.Monitoring.Pipeline.Pipelets;

namespace Puppy.Monitoring.Unit.Tests._helpers
{
    public class GenericPipeline : IPipeline
    {
        public GenericPipeline()
        {
            Events = new List<IEvent>();
        }

        public IList<IEvent> Events { get; private set; }

        public bool WasCalled
        {
            get { return Events.Any(); }
        }

        public void Flow(IEvent @event)
        {
            Events.Add(@event);
        }

        public IPipeline Add(IPipelet pipelet)
        {
            return this;
        }
    }

    public class EventSpecificPipeline : IPipeline<TestEvent>
    {
        public EventSpecificPipeline()
        {
            Events = new List<IEvent>();
        }

        public IList<IEvent> Events { get; private set; }

        public bool WasCalled
        {
            get { return Events.Any(); }
        }

        public void Flow(TestEvent @event)
        {
            Events.Add(@event);
        }

        public void Flow(IEvent @event)
        {
            Flow(@event as TestEvent);
        }

        public IPipeline Add(IPipelet pipelet)
        {
            return this;
        }
    }
}