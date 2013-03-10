using Puppy.Monitoring.Events;
using System;
using Puppy.Monitoring.Tracking;

namespace Puppy.Monitoring
{
	public class TrackingWritingInfoCollector
	{
		IWriteTracking writer;
		Func<string> identifier;
		Track track;

		public TrackingWritingInfoCollector (Track track)
		{
			this.track = track;
		}

		public Track UsingWriter (IWriteTracking trackingWriter)
		{
			this.writer = trackingWriter;
			return track;
		}

		public TrackingWritingInfoCollector WithIdentifier (Func<string> identifier)
		{
			this.identifier = identifier;
			return this;
		}
	}

}