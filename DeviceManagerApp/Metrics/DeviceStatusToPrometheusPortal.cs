using Prometheus;
using DeviceManagerApp.Models;

namespace DeviceManagerApp
{
    public class DeviceStatusToPrometheusPortal
    {
        private static readonly Gauge StreamingDevicesGauge = Metrics.CreateGauge(
            "device_streaming_count",
            "Number of devices currently streaming"
        );

        // GaugeVec med labels til at identificere devices
        private static readonly Gauge DevicesGauge = Metrics.CreateGauge(
            "device_info",
            "Information about devices",
            new GaugeConfiguration
            {
                LabelNames = new[] { "device_id", "ip_address", "status" }
            });

        public static void UpdateMetrics(IEnumerable<Device> devices)
        {
            var deviceList = devices.ToList();

            // Opdater count af streaming devices
            var streamingCount = deviceList.Count(d => d.Status == "Streaming");
            StreamingDevicesGauge.Set(streamingCount);

            // Fjern alle eksisterende labels
            var existingLabels = DevicesGauge.GetAllLabelValues();
            foreach (var labelSet in existingLabels)
            {
                DevicesGauge.RemoveLabelled(labelSet);
            }

            if (deviceList.Count == 0)
            {
                // Hvis ingen devices, eksponer dummy device med værdi 0
                DevicesGauge.WithLabels("none", "none", "none").Set(0);
            }
            else
            {
                // Opdater devices med labels og værdi 1 (indikerer device findes)
                foreach (var device in deviceList)
                {
                    DevicesGauge
                        .WithLabels(device.Id, device.IpAddress ?? "unknown", device.Status ?? "unknown")
                        .Set(1);
                }
            }
        }
    }
}
