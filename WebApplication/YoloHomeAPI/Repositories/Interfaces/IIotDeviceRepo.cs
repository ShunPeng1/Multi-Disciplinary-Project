using YoloHomeAPI.Data;

namespace YoloHomeAPI.Repositories.Interfaces;

public interface IIotDeviceRepo
{
    public Task<IEnumerable<IotDeviceData>> GetAllAsync();
    public Task<IEnumerable<IotDeviceData>> GetAllAsync(string username);
    public Task<IotDeviceData> GetAsync(string username, string type);
    public Task AddAsync(IotDeviceData iotDeviceData);
    public Task DeleteAsync(string username, Guid deviceId);
    
}