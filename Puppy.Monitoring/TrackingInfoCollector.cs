using Puppy.Monitoring.Events;
using System;
using Puppy.Monitoring.Tracking;

namespace Puppy.Monitoring
{

	public class TrackingInfoCollector
	{
		readonly Action action;
		string identifier;
		TrackingConfiguration configuration = new TrackingConfiguration();

		public TrackingInfoCollector (Action action)
		{
			this.action = action;
		}

		public TrackingInfoCollector WithIdentifier (Func<string> identifier)
		{
			configuration = new TrackingConfiguration(this, identifier)
		}

		public void Go ()
		{
			try
			{

			}	
			catch(Exception e)
			{

			}
		}

		public void Testing ()
		{
			Track
				.ThisCall (() => {})
					.WithIdentifier (() => string.Empty)
					.UsingWriter (new FileTrackingWriter ())
				.Go ();



			Track
				.ThisCall (() => {})
					.WithIdentifier (() => string.Empty)
					.UsingWriter(new FileTrackingWriter())
				.Include ()
					.SuccessReporting (() => new SuccessEvent())
					.FailureReporting (() => new FailureEvent())
				.Go ();

		}
	}


}