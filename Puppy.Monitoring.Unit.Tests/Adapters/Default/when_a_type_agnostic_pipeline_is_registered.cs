﻿using Puppy.Monitoring.Adapters;
using Puppy.Monitoring.Adapters.Default;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Adapters.Default
{
    public class when_a_type_agnostic_pipeline_is_registered : Specification
    {
        private readonly IPipelineAdapter adapter;
        private readonly GenericPipeline publisher;

        public when_a_type_agnostic_pipeline_is_registered()
        {
            publisher = new GenericPipeline();

            adapter = new ManualPipelineAdapter()
                .Register(publisher);
        }

        public override void Observe()
        {
            adapter.Push(new TestEvent());
        }

        [Observation]
        public void the_registered_pipeline_is_invoked()
        {
            publisher.WasCalled.ShouldBeTrue();
        }
    }
}