using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Counting
{
    public interface ICountEvents
    {
        int Count { get; }
        void Increment(IEvent @event);
    }
}