using System.Collections.Generic;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets;

namespace Puppy.Monitoring.Pipeline
{
    public class LinearPipeline : IPipeline
    {
        private static readonly ILog log = LogManager.GetLogger<LinearPipeline>();
        private readonly List<IPipelet> pipelets = new List<IPipelet>();

        public LinearPipeline(IEnumerable<IPipelet> pipelets)
        {
            this.pipelets.AddRange(pipelets);
        }

        public void Flow(IEvent @event)
        {
            foreach (var pipelet in pipelets)
            {
                log.DebugFormat("Flowing {0} to {1}", @event.GetType(), pipelet.GetType());
                pipelet.Flow(@event);
            }
        }

        public IPipeline Add(IPipelet pipelet)
        {
            pipelets.Add(pipelet);
            return this;
        }
    }
}