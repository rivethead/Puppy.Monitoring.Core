using System;
using System.Collections.Generic;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline;

namespace Puppy.Monitoring.Adapters.Default
{
    internal class TypeSpecificPipelineContainer : IContainPipelines
    {
        private readonly IDictionary<Type, IList<IPipeline>> pipelines =
            new Dictionary<Type, IList<IPipeline>>();

        private static readonly ILog log = LogManager.GetLogger<TypeSpecificPipelineContainer>();

        public void Push(IEvent @event)
        {
            if (pipelines.Count == 0)
            {
                log.WarnFormat("The {0} container does not have any pipelines", GetType());
                return;
            }

            if (!pipelines.ContainsKey(@event.GetType()))
            {
                log.WarnFormat("Could not find a type specific pipeline for {0}", @event.GetType());
                return;
            }

            var availablePipelines = pipelines[@event.GetType()];
            foreach (var pipeline in availablePipelines)
            {
                log.InfoFormat("Flowing {0} down the {1} pipeline", @event.GetType(), pipeline.GetType());
                pipeline.Flow(@event);
            }
        }

        public void Register(Type type, IPipeline pipeline)
        {
            if (pipeline == null)
                throw new ArgumentNullException("pipeline");

            if (pipelines.ContainsKey(type))
            {
                pipelines[type].Add(pipeline);
                return;
            }

            pipelines.Add(type, new List<IPipeline>
                {
                    pipeline
                });
        }
    }
}