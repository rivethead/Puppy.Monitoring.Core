using System.Collections.Generic;
using System.Linq;
using Moq;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Pipeline.Pipelets.Notification;
using Puppy.Monitoring.Unit.Tests._helpers;
using Xunit.Extensions;

namespace Puppy.Monitoring.Unit.Tests.Pipelines.Pipelets.Notification
{
    public class when_flowing_an_event_through_the_notification_pipelet_and_filter_is_valid : Specification
    {
        private readonly NotificationPipelet pipelet;
        private IEnumerable<IEvent> resulting_events;
        private readonly Mock<INotifyBasedOnEvent> notifier;

        public when_flowing_an_event_through_the_notification_pipelet_and_filter_is_valid()
        {
            notifier = new Mock<INotifyBasedOnEvent>();

            pipelet = new NotificationPipelet(notifier.Object, new TestEventSpecification());
        }

        public override void Observe()
        {
            resulting_events = pipelet.Flow(new TestEvent());
        }

        [Observation]
        public void the_notifier_is_invoked()
        {
            notifier.Verify(n => n.Raise(It.IsAny<TestEvent>()));
        }

        [Observation]
        public void an_notification_raised_event_is_available_to_the_pipeline()
        {
            resulting_events.Count().ShouldEqual(1);
            resulting_events.First().ShouldBeType<NotificationRaisedEvent>();
        }
    }
}