namespace YoloHomeAPI.Controllers;

public interface IAdafruitMqttService
{

    public void PublishMessage(string topic, string content);

}