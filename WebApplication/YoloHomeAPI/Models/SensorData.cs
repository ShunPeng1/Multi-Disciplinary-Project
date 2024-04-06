namespace YoloHomeAPI.Data;

public class SensorData
{   
    public DateTime TimeStamp { get; set; }
    public string Value { get; set; } = null!;
    public Guid DeviceId { get; set; }
}