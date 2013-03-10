using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Puppy.Monitoring.Tracking;

namespace Puppy.Monitoring
{
    public class TrackWritingInfoCollector<TResponse>
    {
        private readonly Track<TResponse> track;
        private readonly TrackReportInfoCollector<TResponse> trackReportInfoCollector;
        private Func<string> identifier = () => "unknown";
        private string request = string.Empty;
        private Func<TResponse, string> serialisation = response => response.ToString();
        private IWriteTracking writer = new NullTrackingWriter();

        public TrackWritingInfoCollector(Track<TResponse> track)
        {
            this.track = track;
            trackReportInfoCollector = new TrackReportInfoCollector<TResponse>(this);
        }

        public TrackWritingInfoCollector<TResponse> Write()
        {
            return this;
        }

        public TrackWritingInfoCollector<TResponse> With(IWriteTracking writer)
        {
            this.writer = writer;
            return this;
        }

        public TrackWritingInfoCollector<TResponse> TheRequest(string request)
        {
            this.request = request;
            return this;
        }

        public TrackWritingInfoCollector<TResponse> TheRequest<TRequest>(TRequest request)
        {
            using (var stringWriter = new StringWriter())
            {
                var serialiser = new XmlSerializer(request.GetType());
                serialiser.Serialize(stringWriter, request);
                this.request = stringWriter.GetStringBuilder().ToString();
            }

            return this;
        }

        public TrackWritingInfoCollector<TResponse> TheRequest<TRequest>(TRequest request, IXmlSerializable serialiser)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var xmlStream = new XmlTextWriter(stringWriter))
                {
                    serialiser.WriteXml(xmlStream);
                    this.request = stringWriter.GetStringBuilder().ToString();
                }
            }

            return this;
        }

        public TrackReportInfoCollector<TResponse> TheResponse(Func<TResponse, string> serialisation)
        {
            this.serialisation = serialisation;
            return trackReportInfoCollector;
        }

        public TrackReportInfoCollector<TResponse> TheResponse(Func<TResponse, IXmlSerializable> serialiser)
        {
            this.serialisation = r =>
                {
                    using (var stream = new StringWriter())
                    {
                        using (var xmlStream = new XmlTextWriter(stream))
                        {
                            serialiser(r).WriteXml(xmlStream);
                            return stream.GetStringBuilder().ToString();
                        }
                    }
                };

            return trackReportInfoCollector;
        }

        public TResponse Go()
        {
            return track.Go();
        }

        internal void Execute(TResponse callResponse)
        {
            writer.Write(identifier(),
                         request,
                         serialisation(callResponse));
        }

        public TrackWritingInfoCollector<TResponse> UsingAsIdentifier(Func<string> identifier)
        {
            this.identifier = identifier;
            return this;
        }

        internal List<Func<ReportInfoCollector>> Successes()
        {
            return trackReportInfoCollector.Successes;
        }

        internal List<Func<ReportInfoCollector>> Failures()
        {
            return trackReportInfoCollector.Failures;
        }
    }
}