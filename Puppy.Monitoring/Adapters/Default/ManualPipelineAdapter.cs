using System;
using Common.Logging;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline;

namespace Puppy.Monitoring.Adapters.Default
{
    public class ManualPipelineAdapter : IPipelineAdapter
    {
        private static readonly ILog log = LogManager.GetLogger<ManualPipelineAdapter>();

        private readonly TypeAgnosticPipelineContainer agnosticPipelines = new TypeAgnosticPipelineContainer();
        private readonly TypeSpecificPipelineContainer typeSpecificPipelines = new TypeSpecificPipelineContainer();
        private readonly IContainPipelines[] publishingSources;

        public ManualPipelineAdapter()
        {
            publishingSources = new IContainPipelines[]
                {
                    agnosticPipelines,
                    typeSpecificPipelines
                };
        }

        public void Push(IEvent @event)
        {
            foreach (var publisherSource in publishingSources)
                publisherSource.Push(@event);
        }

        public ManualPipelineAdapter Register<TEvent>(IPipeline<TEvent> pipeline) where TEvent : IEvent
        {
            ValidatePipeline(pipeline);

            log.DebugFormat("Adding {0} to the type specific pipelines", pipeline.GetType());

            typeSpecificPipelines.Register(typeof(TEvent), pipeline);
            return this;
        }

        public IPipelineAdapter Register(IPipeline pipeline)
        {
            ValidatePipeline(pipeline);

            log.DebugFormat("Adding {0} to the generic specific pipelines", pipeline.GetType());

            agnosticPipelines.Register(pipeline);
            return this;
        }

        private void ValidatePipeline(IPipeline pipeline)
        {
            if (pipeline != null)
                return;

            var message = string.Format("Invalid pipeline to be registered. The pipeline is null");
            log.FatalFormat(message);

            throw new Exception(message);
        }
    }
}