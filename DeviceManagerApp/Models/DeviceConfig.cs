namespace DeviceManagerApp.Models
{
    public class DeviceConfig
    {
        public string Server { get; set; } = string.Empty;
        public int AudioBitrate { get; set; }
        public int VideoBitrate { get; set; }
        public string Resolution { get; set; } = string.Empty;
        public int Fps { get; set; }
        public string Codec { get; set; } = string.Empty;

    }
}
