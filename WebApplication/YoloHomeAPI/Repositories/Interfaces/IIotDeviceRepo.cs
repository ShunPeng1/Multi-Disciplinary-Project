using YoloHomeAPI.Data;

namespace YoloHomeAPI.Repositories.Interfaces;

public interface IIotDeviceRepo
{
    public Task<IEnumerable<IotDeviceData>> GetAllAsync();
    public Task<IEnumerable<IotDeviceData>> GetAllByUserNameAsync(string username);
    public Task AddAsync(IotDeviceData iotDeviceData);
    public Task DeleteAsync(Guid deviceId);
    
}