using DeviceManagerApp.Helpers;
using DeviceManagerApp.Models;
using System.Text.Json;

public class APIManager
{
    private readonly HttpClient _httpClient;

    public APIManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Hent kun status (kun "status" felt bruges)
    public async Task<string?> GetStatusAsync(string ip)
    {
        try
        {
            var url = new ApiUrlBuilder()
                .WithIp(ip)
                .ForStatus()
                .Build();

            var json = await _httpClient.GetFromJsonAsync<JsonElement>(url);
            return json.GetProperty("status").GetString();
        }
        catch
        {
            return null;
        }
    }

    // Hent konfiguration
    public async Task<DeviceConfig?> GetConfigAsync(string ip)
    {
        try
        {
            var url = new ApiUrlBuilder()
                .WithIp(ip)
                .ForConfig()
                .Build();

            return await _httpClient.GetFromJsonAsync<DeviceConfig>(url);
        }
        catch
        {
            return null;
        }
    }
}