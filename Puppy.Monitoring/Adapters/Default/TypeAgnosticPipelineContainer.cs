using System.Collections.Generic;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline;

namespace Puppy.Monitoring.Adapters.Default
{
    internal class TypeAgnosticPipelineContainer : IContainPipelines
    {
        private static readonly ILog log = LogManager.GetLogger<TypeAgnosticPipelineContainer>();
        private readonly IList<IPipeline> pipelines = new List<IPipeline>();

        public void Push(IEvent @event)
        {
            if (pipelines.Count == 0)
            {
                log.WarnFormat("The {0} container does not have any pipelines", GetType());
                return;
            }

            foreach (var pipeline in pipelines)
            {
                log.InfoFormat("Flowing {0} down the {1} pipeline", @event.GetType(), pipeline.GetType());
                pipeline.Flow(@event);
            }
        }

        public void Register(IPipeline pipeline)
        {
            pipelines.Add(pipeline);
        }
    }
}