using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets;

namespace Puppy.Monitoring.Pipeline
{
    public interface IPipeline
    {
        void Flow(IEvent @event);
        IPipeline Add(IPipelet pipelet);
    }

    public interface IPipeline<in TEvent> : IPipeline where TEvent : IEvent
    {
        void Flow(TEvent @event);
    }
}