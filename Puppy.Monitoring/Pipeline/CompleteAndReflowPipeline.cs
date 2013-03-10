using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets;

namespace Puppy.Monitoring.Pipeline
{
    public class CompleteAndReflowPipeline : IPipeline
    {
        private static readonly ILog log = LogManager.GetLogger<CompleteAndReflowPipeline>();
        private readonly List<IPipelet> pipelets = new List<IPipelet>();

        public CompleteAndReflowPipeline(IEnumerable<IPipelet> pipelets)
        {
            this.pipelets.AddRange(pipelets);
        }

        public void Flow(IEvent @event)
        {
            var eventsToFlow = new List<IEvent>();
            foreach (var pipelet in pipelets)
            {
                log.DebugFormat("Flowing {0} to {1}", @event.GetType(), pipelet.GetType());
                var resultingEvents = pipelet.Flow(@event);

                if (resultingEvents.Any(e => e.GetType() != typeof (NoopEvent)))
                {
                    log.InfoFormat("Adding {0} events to the pipeline for later flowing, after flowing {1} to {2}",
                                   resultingEvents.Count(),
                                   @event.GetType(),
                                   pipelet.GetType());

                    eventsToFlow.AddRange(resultingEvents);
                }
            }

            log.DebugFormat("Flowing resulting events to the pipeline. There are {0} events to reflow",
                            eventsToFlow.Count);
            foreach (var eventToFlow in eventsToFlow)
            {
                Flow(eventToFlow);
            }
        }

        public IPipeline Add(IPipelet pipelet)
        {
            pipelets.Add(pipelet);
            return this;
        }
    }
}