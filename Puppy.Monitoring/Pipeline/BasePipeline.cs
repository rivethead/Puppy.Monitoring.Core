using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets;

namespace Puppy.Monitoring.Pipeline
{
    public abstract class BasePipeline : IPipeline
    {
        private readonly List<IPipelet> pipelets = new List<IPipelet>();
        private static readonly ILog log = LogManager.GetLogger<BasePipeline>();

        protected BasePipeline()
        {
        }

        protected BasePipeline(IEnumerable<IPipelet> pipelets)
        {
            this.pipelets.AddRange(pipelets);
        }

        public void Flow(IEvent @event)
        {
            Flow(@event, pipelets);
        }

        public IPipeline Add(IPipelet pipelet)
        {
            pipelets.Add(pipelet);
            return this;
        }

        protected IEnumerable<IPipelet> RebuildPipeline(IEvent @event)
        {
            log.InfoFormat("Rebuilding pipeline for event {0}", @event.GetType());

            var rebuildPipeline = pipelets.Where(p => p.WantsEvent(@event)).ToList();
            return rebuildPipeline;
        }

        protected abstract void Flow(IEvent @event, IEnumerable<IPipelet> pipeline);
    }
}