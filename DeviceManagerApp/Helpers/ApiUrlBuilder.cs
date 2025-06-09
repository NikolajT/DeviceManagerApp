namespace DeviceManagerApp.Helpers;


// Helper-klasse til at bygge API-URL'er baseret på IP og formål

public class ApiUrlBuilder
{
    private string _ip = "";
    private string _path = "";

    // Sætter IP-adressen for enheden som URL'en skal bygges op omkring.

    public ApiUrlBuilder WithIp(string ip)
    {
        _ip = ip;
        return this;
    }

    // Bygger URL'en til at hente status fra enheden

    public ApiUrlBuilder ForStatus()
    {
        _path = "control/api/v1/livestreams/0";
        return this;
    }

    /* Bygger URL'en til at hente konfiguration fra Device */

    public ApiUrlBuilder ForConfig()
    {
        _path = "control/api/v1/livestreams/customPlatforms/Custom.json";
        return this;
    }


    /* Genererer den endelige URL som en string.*/

    public string Build()
    {
        if (string.IsNullOrWhiteSpace(_ip) || string.IsNullOrWhiteSpace(_path))
            throw new InvalidOperationException("IP and Path must be set");

        return $"https://{_ip}/{_path}";
    }
}