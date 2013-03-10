using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets;

namespace Puppy.Monitoring.Pipeline
{
    public class QueuedEventsPipeline : BasePipeline
    {
        private readonly Queue<IEvent> queue = new Queue<IEvent>();
        private static readonly ILog log = LogManager.GetLogger<QueuedEventsPipeline>();

        public QueuedEventsPipeline()
        {
        }

        public QueuedEventsPipeline(IEnumerable<IPipelet> pipelets) : base(pipelets)
        {
        }

        protected override void Flow(IEvent @event, IEnumerable<IPipelet> pipeline)
        {
            queue.Enqueue(@event);

            ProcessQueue(pipeline);
        }

        private void ProcessQueue(IEnumerable<IPipelet> pipeline)
        {
            while (queue.Count > 0)
            {
                var @event = queue.Dequeue();
                foreach (var pipelet in pipeline)
                {
                    log.DebugFormat("Flowing {0} to {1}", @event.GetType(), pipelet.GetType());
                    var resultingEvents = pipelet.Flow(@event);

                    if (resultingEvents.Any(e => e.GetType() != typeof(NoopEvent)))
                    {
                        log.InfoFormat("Adding {0} events to the queue to flow into the pipeline after flowing {1} to {2}",
                                       resultingEvents.Count(),
                                       @event.GetType(),
                                       pipelet.GetType());

                        foreach (var resultingEvent in resultingEvents)
                            queue.Enqueue(resultingEvent);
                    }
                    
                }
            }
        }
    }
}