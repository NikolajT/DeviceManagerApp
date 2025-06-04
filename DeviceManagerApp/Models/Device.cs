namespace DeviceManagerApp.Models
{
    public class Device
    {
        public string Id { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public DeviceStatus? Status { get; set; }
        public DeviceConfig? Config { get; set; }
    }
}
