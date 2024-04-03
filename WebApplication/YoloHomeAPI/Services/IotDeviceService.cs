namespace YoloHomeAPI.Services.Interfaces;

public class IotDeviceService : IIotDeviceService
{
    public IIotDeviceService.IotDeviceResult GetAll(string username)
    {
        return new IIotDeviceService.IotDeviceResult(true, "Success");

    }

    public IIotDeviceService.IotDeviceResult Get(string username)
    {
        throw new NotImplementedException();
    }
}