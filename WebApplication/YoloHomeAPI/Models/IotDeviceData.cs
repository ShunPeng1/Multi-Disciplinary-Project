namespace YoloHomeAPI.Data;

public class IotDeviceData
{   
    public Guid DeviceId { get; set; }
    public string DeviceName { get; set; } = null!;
    public string DeviceLocation { get; set; } = null!;
    public string DeviceType { get; set; } = null!;
    public string DeviceState { get; set; } = null!;

}