using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Counting
{
    public class NullCounter : ICountEvents
    {
        public int Count
        {
            get { return 0; }
        }

        public void Increment(IEvent @event)
        {
        }
    }
}