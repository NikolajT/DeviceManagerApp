using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using DeviceManagerApp.Services;

namespace DeviceManagerApp.Workers
{
    public class DeviceMetricsWorker : BackgroundService
    {
        private readonly DeviceManager _deviceManager;

        public DeviceMetricsWorker(DeviceManager deviceManager)
        {
            _deviceManager = deviceManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Kører indtil applikationen stopper
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Opdater status og config for alle enheder
                    await _deviceManager.UpdateAllStatusesAsync();
                    await _deviceManager.UpdateAllConfigsAsync();
                    // DEBUGGING
                    foreach (var device in _deviceManager.Devices)
                    {
                        Console.WriteLine($"Device ID: {device.Id}, IP: {device.IpAddress}, Status: {device.Status}");
                    }

                    // Opdater Prometheus metrics med den nyeste liste
                    DeviceStatusToPrometheusPortal.UpdateMetrics(_deviceManager.Devices);
                }
                catch (Exception ex)
                {
                    // Log evt. fejl 
                    Console.WriteLine($"Error in DeviceMetricsWorker: {ex.Message}");
                }

               
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
