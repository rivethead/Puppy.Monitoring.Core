using System;
using Puppy.Monitoring.Events;
using Puppy.Monitoring.Publishing;

namespace Puppy.Monitoring.Unit.Tests._helpers
{
    [Serializable]
    public class TestEvent : Event
    {
        public TestEvent(PublishingContext context, EventTiming eventAudit, Categorisation categorisation, Guid correlationId, Timings timings,
                         Guid id) : base(context, eventAudit, categorisation, correlationId, timings, id)
        {
        }

        public TestEvent() : base(SystemTime.Now(),
                                  new Categorisation("Category", "SubCategory"), new Timings(0))
        {
        }

        public TestEvent(Categorisation categorisation)
            : base(SystemTime.Now(), categorisation, new Timings(0))
        {
        }

        public TestEvent(Categorisation categorisation, Timings timings) : base(SystemTime.Now(), categorisation, timings)
        {
        }

        public string Description { get; set; }
    }
}