using System;

namespace Puppy.Monitoring.Tracking
{
	public interface IWriteTracking : IDisposable
	{
        void Write(string identifier, string request, string response, bool overwrite = true);
	}
}