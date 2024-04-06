namespace YoloHomeAPI.Data;

public class AdafruitDataReceiveData
{
    public string Topic { get; set; } = null!;
    public string RawMessage { get; set; } = null!;
    public List<float> Values { get; set; } = null!;
}