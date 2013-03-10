using System;
using Puppy.Monitoring.Tracking;

namespace Puppy.Monitoring
{
    public class Track<TResponse>
    {
        private readonly Func<TResponse> call;
        private static TrackWritingInfoCollector<TResponse> writingInfoCollector;

        private Track(Func<TResponse> call)
        {
            this.call = call;
        }

        public static TrackWritingInfoCollector<TResponse> Call(Func<TResponse> call)
        {
            writingInfoCollector = new TrackWritingInfoCollector<TResponse>(new Track<TResponse>(call));
            return writingInfoCollector;
        }

        private TResponse WrappedCall()
        {
            var callResponse = call();
            writingInfoCollector.Execute(callResponse);

            return callResponse;
        }

        public TResponse Go()
        {
            return Measure
                .This<TResponse>(WrappedCall)
                .OnSuccess(writingInfoCollector.Successes())
                .OnFailure(writingInfoCollector.Failures())
                .Gauge();
        }

        public void Testing()
        {
            Track<string>
                .Call(() => string.Empty)
                .Write()
                    .With(new FileTrackingWriter(null))
                    .UsingAsIdentifier(() => string.Empty)
                    .TheRequest(string.Empty)
                    .TheResponse(r => string.Empty)
                .Report()
                    .Success(null)
                    .Failure(null)
                .Go();
        }
    }
}