using Prometheus;
using DeviceManagerApp.Models;

namespace DeviceManagerApp
{
    public class DeviceStatusToPrometheusPortal
    {
        // Fix: Use the correct method from the Prometheus library to create a Gauge (antallet af devices med status "Streaming")
        private static readonly Gauge StreamingDevicesGauge = Metrics.CreateGauge(
            "device_streaming_count",
            "Number of devices currently streaming"
        );

        public static void UpdateMetrics(IEnumerable<Device> devices)
        {
            var streamingCount = devices.Count(d => d.Status == "Streaming");
            StreamingDevicesGauge.Set(streamingCount);
        }
    }
}
