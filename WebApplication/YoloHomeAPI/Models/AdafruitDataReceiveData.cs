namespace YoloHomeAPI.Data;

public class AdafruitDataReceiveData
{
    public string Topic { get; set; }
    public string RawMessage { get; set; }
    public List<float> Values { get; set; }
}