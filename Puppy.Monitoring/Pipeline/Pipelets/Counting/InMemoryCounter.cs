using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Counting
{
    public class InMemoryCounter : ICountEvents
    {
        public int Count { get; private set; }

        public void Increment(IEvent @event)
        {
            Count++;
        }
    }
}