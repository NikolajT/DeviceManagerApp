using DeviceManagerApp.Models;
public class DeviceManager
{
    public List<Device> Devices { get; } = new List<Device>();

    public void AddDevice(Device device)
    {
        Devices.Add(device);
    }
}
