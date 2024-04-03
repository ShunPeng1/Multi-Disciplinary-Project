namespace YoloHomeAPI.Controllers;

public interface IAdafruitMqttService
{
    public class AdafruitMqttResult
    {
        public AdafruitMqttResult(bool isSuccess , string response)
        {
            IsSuccess = isSuccess;
            Response = response;
        }

        public bool IsSuccess { get; set; }
        public string Response { get; set; }
    }
    
    public AdafruitMqttResult Execute(string command);
}