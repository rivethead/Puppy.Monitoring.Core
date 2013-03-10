using System;
using System.IO;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing.Default;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Publishing.Default.BinaryFilePublishing
{
    public class when_publishing_a_null_event_to_the_binary_file_publisher : Specification
    {
        private readonly IEvent expected_event;
        private readonly string file_path;
        private readonly BinaryFilePublisher publisher;
        private Exception actual_exception;

        public when_publishing_a_null_event_to_the_binary_file_publisher()
        {
            file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp_stream_test.txt");
            if (File.Exists(file_path))
                File.Delete(file_path);

            publisher = new BinaryFilePublisher(file_path);
            expected_event = null;
        }

        public override void Observe()
        {
            try
            {
                publisher.Publish(expected_event);
            }
            catch (Exception e)
            {
                actual_exception = e;
            }
        }

        [Observation]
        public void an_exception_is_thrown()
        {
            actual_exception.ShouldNotBeNull();
        }

        [Observation]
        public void an_argument_null_exception_is_thrown()
        {
            actual_exception.ShouldBeType<ArgumentNullException>();
        }


        [Observation]
        public void an_exception_is_thrown_with_a_usefull_message()
        {
            actual_exception.Message
                .Contains("Event is null")
                .ShouldBeTrue();
        }


    }
}