using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Builders
{
    public interface IBuildReportingEvent
    {
        IEvent Build();
    }
}