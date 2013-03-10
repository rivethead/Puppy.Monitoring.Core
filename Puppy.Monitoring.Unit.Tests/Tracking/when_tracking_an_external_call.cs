using System;
using Moq;
using Puppy.Monitoring.Adapters;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;
using Puppy.Monitoring.Tracking;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Tracking
{
    public class when_tracking_an_external_call : Specification
    {
        private readonly ExternalCallResponse expected_response;
        private readonly string identifier;
        private readonly string request;
        private readonly Mock<IWriteTracking> tracking_writer;
        private readonly Mock<IPipelineAdapter> pipeline_adapter;
        private bool was_called;

        public when_tracking_an_external_call()
        {
            tracking_writer = new Mock<IWriteTracking>();

            identifier = "xxx";
            request = "This is the request";
            expected_response = new ExternalCallResponse(1, "hello");

            pipeline_adapter = new Mock<IPipelineAdapter>();
            Publisher.Use(pipeline_adapter.Object, new PublishingContext("TEST", "TEST"));
        }

        public override void Observe()
        {
            Track<ExternalCallResponse>
                .Call(() => fake_external_call(request))
                .Write()
                    .With(tracking_writer.Object)
                    .UsingAsIdentifier(() => identifier)
                    .TheRequest(request)
                    .TheResponse(response => response.ToString())
                .Report()
                    .Success(Report.Success)
                .Go();
        }

        [Observation]
        public void the_success_event_is_published()
        {
            pipeline_adapter
                .Verify(a => a.Push(It.IsAny<IEvent>()));
        }


        [Observation]
        public void the_external_system_is_called()
        {
            was_called.ShouldBeTrue();
        }

        [Observation]
        public void the_request_is_written_using_the_supplied_writer()
        {
            tracking_writer
                .Verify(w => w.Write(It.Is<string>(s => s.Equals(identifier)),
                                     It.Is<string>(r => r.Equals(request)),
                                     It.Is<string>(r => r.Equals(expected_response.ToString())),
                                     It.IsAny<bool>()));
        }


        private ExternalCallResponse fake_external_call(string request)
        {
            was_called = true;
            return expected_response;
        }
    }

    [Serializable]
    public class ExternalCallResponse
    {
        public ExternalCallResponse()
        {
        }

        public ExternalCallResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Id, Name);
        }
    }
}