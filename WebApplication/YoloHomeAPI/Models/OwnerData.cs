namespace YoloHomeAPI.Data;

public class OwnerData
{   
    public string UserName { get; set; } = null!;
    public Guid DeviceId { get; set; }
    public string Recurrence { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
}