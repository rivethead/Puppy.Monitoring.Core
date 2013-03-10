using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets;

namespace Puppy.Monitoring.Pipeline
{
    public class ReflowOnEventPipeline : BasePipeline
    {
        private static readonly ILog log = LogManager.GetLogger<ReflowOnEventPipeline>();

        public ReflowOnEventPipeline(IEnumerable<IPipelet> pipelets) : base(pipelets)
        {
        }

        protected override void Flow(IEvent @event, IEnumerable<IPipelet> pipeline)
        {
            foreach (var pipelet in pipeline)
            {
                log.DebugFormat("Flowing {0} to {1}", @event.GetType(), pipelet.GetType());
                var resultingEvents = pipelet.Flow(@event);

                if (resultingEvents.Any(e => e.GetType() != typeof (NoopEvent)))
                {
                    log.InfoFormat("Adding {0} events immediately to the pipeline after flowing {1} to {2}",
                                   resultingEvents.Count(),
                                   @event.GetType(),
                                   pipelet.GetType());

                    foreach (var resultingEvent in resultingEvents)
                        Flow(resultingEvent);
                }
            }
        }
    }
}