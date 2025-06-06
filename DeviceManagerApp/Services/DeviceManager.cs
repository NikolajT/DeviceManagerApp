using DeviceManagerApp.Models;
using DeviceManagerApp.Services;

namespace DeviceManagerApp.Services;

// DeviceManager er ansvarlig for at holde styr på alle devices og hente deres status og konfiguration.

public class DeviceManager
{
    private readonly APIManager _apiManager;

    // Liste over alle kendte enheder
    public List<Device> Devices { get; } = new();

    public DeviceManager(APIManager apiManager)
    {
        _apiManager = apiManager;
    }

    public void AddDevice(Device device)
    {
        Devices.Add(device);
    }

    /// Henter status for én enhed og opdaterer den i listen.

    public async Task<string?> GetStatusAsync(string ip)
    {
        var status = await _apiManager.GetStatusAsync(ip);

        var device = Devices.FirstOrDefault(d => d.IpAddress == ip);
        if (device != null && status != null)
        {
            device.Status = status;
            device.UpdatedAt = DateTime.UtcNow;
        }

        return status;
    }

    /// Henter konfigurationen for én enhed og opdaterer den i listen.

    public async Task<DeviceConfig?> GetConfigAsync(string ip)
    {
        var config = await _apiManager.GetConfigAsync(ip);

        var device = Devices.FirstOrDefault(d => d.IpAddress == ip);
        if (device != null && config != null)
        {
            device.Config = config;
        }

        return config;
    }

    /// Henter status fra alle enheder og opdaterer internt.

    public async Task UpdateAllStatusesAsync()
    {
        var tasks = Devices.Select(d => GetStatusAsync(d.IpAddress));
        await Task.WhenAll(tasks);
    }

    /// Henter konfiguration fra alle enheder og opdaterer internt.
    public async Task UpdateAllConfigsAsync()
    {
        var tasks = Devices.Select(d => GetConfigAsync(d.IpAddress));
        await Task.WhenAll(tasks);
    }
}