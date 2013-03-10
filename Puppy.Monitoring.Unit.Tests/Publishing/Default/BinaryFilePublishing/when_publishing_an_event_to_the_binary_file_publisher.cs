using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing.Default;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Publishing.Default.BinaryFilePublishing
{
    public class when_publishing_an_event_to_the_binary_file_publisher : Specification
    {
        private readonly IEvent expected_event;
        private readonly string file_path;
        private readonly BinaryFilePublisher publisher;

        public when_publishing_an_event_to_the_binary_file_publisher()
        {
            file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp_stream_test.txt");
            if (File.Exists(file_path))
                File.Delete(file_path);

            publisher = new BinaryFilePublisher(file_path);
            expected_event = new TestEvent()
                {
                    Description = "This is a test"
                };
        }

        public override void Observe()
        {
            publisher.Publish(expected_event);
        }

        [Observation]
        public void the_event_is_written_to_the_file_stream()
        {
            File.Exists(file_path).ShouldBeTrue();

            using (var stream = new FileStream(file_path, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                var actual_event = formatter.Deserialize(stream) as TestEvent;

                actual_event.ShouldNotBeNull();
            }
        }
    }
}