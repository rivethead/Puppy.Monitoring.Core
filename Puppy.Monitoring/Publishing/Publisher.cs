using System;
using Common.Logging;
using Puppy.Monitoring.Adapters;
using Puppy.Monitoring.Adapters.Default;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Publishing
{
    public class Publisher
    {
        private static IPipelineAdapter pipelineAdapter = new NullPipelineAdapter();
        private static PublishingContext publishingContext;

        private static readonly ILog log = LogManager.GetLogger<Publisher>();

        public static void Use(IPipelineAdapter adapter, PublishingContext context)
        {
            pipelineAdapter = adapter;
            publishingContext = context;
        }

        public static void Reset()
        {
            pipelineAdapter = null;
            publishingContext = null;
        }

        public void Publish(IEvent @event)
        {
            if (@event == null)
            {
                const string message = "The event to be published is null";
                log.FatalFormat(message);
                throw new Exception(message);
            }

            if (pipelineAdapter == null)
            {
                const string message =
                    "The pipeline adapter is null. To set the pipeline adapter use the Publisher.Use method";
                log.FatalFormat(message);
                throw new Exception(message);
            }

            log.DebugFormat("Pushing {0} down the pipeline using adapter {1}", @event.GetType(),
                            pipelineAdapter.GetType());

            @event.AttachContext(publishingContext);

            pipelineAdapter.Push(@event);
        }
    }
}