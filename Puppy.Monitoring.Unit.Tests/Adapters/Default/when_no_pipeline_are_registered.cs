using System;
using Puppy.Monitoring.Adapters;
using Puppy.Monitoring.Adapters.Default;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Adapters.Default
{
    public class when_no_pipeline_are_registered : Specification
    {
        private readonly IPipelineAdapter adapter;
        private Exception actual_exception;

        public when_no_pipeline_are_registered()
        {
            adapter = new ManualPipelineAdapter();
        }

        public override void Observe()
        {
            try
            {
                adapter.Push(new TestEvent());
            }
            catch (Exception e)
            {
                actual_exception = e;
            }
        }

        [Observation]
        public void no_exception_is_thrown()
        {
            actual_exception.ShouldBeNull();
        }
    }
}