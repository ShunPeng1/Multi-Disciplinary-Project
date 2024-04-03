namespace YoloHomeAPI.Data;

public class ActivityLogData
{
    public string UserName { get; set; } = null!;
    public string Activity { get; set; } = null!;
    public DateTime TimeStamp { get; set; } = DateTime.Now;
    
}