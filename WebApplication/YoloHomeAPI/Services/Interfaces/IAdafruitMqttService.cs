using YoloHomeAPI.Data;

namespace YoloHomeAPI.Controllers;

public interface IAdafruitMqttService
{
    
    public Action<AdafruitDataReceiveData> OnDoorMessageReceived { get; set; }
    public Action<AdafruitDataReceiveData> OnFanReceived { get; set; }
    public Action<AdafruitDataReceiveData> OnLightMessageReceived { get; set; }
    public Action<AdafruitDataReceiveData> OnHumidityMessageReceived { get; set; }
    public Action<AdafruitDataReceiveData> OnTemperatureMessageReceived { get; set; }

    
    public void PublishMessage(string topic, string content);

}