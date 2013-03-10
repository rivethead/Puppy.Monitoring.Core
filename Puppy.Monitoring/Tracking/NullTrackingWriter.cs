namespace Puppy.Monitoring.Tracking
{
    public class NullTrackingWriter : IWriteTracking
    {
        public void Dispose()
        {
        }

        public void Write(string identifier, string request, string response, bool overwrite = true)
        {
        }
    }
}