namespace Puppy.Monitoring.Tracking
{
    public interface IDefineTrackingNamingConvention
	{
        string RequestFileName(string identifier, string uniqueIdentifier);
        string ResponseFileName(string identifier, string uniqueIdentifier);

        string RequestFileName(string identifier);
        string ResponseFileName(string identifier);
	}

	
}