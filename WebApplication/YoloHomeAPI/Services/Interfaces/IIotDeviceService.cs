using YoloHomeAPI.Data;

namespace YoloHomeAPI.Services.Interfaces;

public interface IIotDeviceService
{
    public class IotDeviceResult
    {
        public IotDeviceResult(bool isSuccess , List<IotDeviceData> response)
        {
            IsSuccess = isSuccess;
            Response = response;
        }

        public bool IsSuccess { get; set; }
        public List<IotDeviceData> Response { get; set; }
    }
    
    public class SensorDataResult
    {
        public SensorDataResult(bool isSuccess , List<string> response)
        {
            IsSuccess = isSuccess;
            Response = response;
        }

        public bool IsSuccess { get; set; }
        public List<string> Response { get; set; }
    }
    
    public Task<IotDeviceResult> AddOwner(string username);
    
    public Task<IotDeviceResult> GetAllDevices(string username);
    public Task<SensorDataResult> GetAllSensorData(Guid deviceId, DateTime start, DateTime end);
    
}