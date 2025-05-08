using System.Diagnostics.Metrics;
using System.Reflection;
using System.Xml.Linq;

namespace Adapters.Outbound.Metrics
{
    public class MetricsAdapter
    {
        private readonly Meter _meter;
        private readonly Counter<long> _requestCounter;
        private readonly Histogram<double> _requestDuration;
        public MetricsAdapter()
        {
            _meter = new Meter(Assembly.GetExecutingAssembly().GetName().Name);

            _requestCounter = _meter.CreateCounter<long>("app_requests_total",
                description: "Total number of requests");

            _requestDuration = _meter.CreateHistogram<double>("app_request_duration_seconds",
                description: "Request duration in seconds");
        }

        public void RecordRequest(string endpoint)
        {
            _requestCounter.Add(1, new KeyValuePair<string, object>("endpoint", endpoint));
        }

        public void RecordRequestDuration(double duration, string endpoint)
        {
            _requestDuration.Record(duration,
                new KeyValuePair<string, object>("endpoint", endpoint));
        }
    }
}
