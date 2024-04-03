namespace YoloHomeAPI.Services.Interfaces;

public interface IIotDeviceService
{
    public class IotDeviceResult
    {
        public IotDeviceResult(bool isSuccess , string response)
        {
            IsSuccess = isSuccess;
            Response = response;
        }

        public bool IsSuccess { get; set; }
        public string Response { get; set; }
    }
    
    public IotDeviceResult GetAll(string username);
    public IotDeviceResult Get(string username);
    
}