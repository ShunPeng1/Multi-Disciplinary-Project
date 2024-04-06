using YoloHomeAPI.Data;

namespace YoloHomeAPI.Repositories.Interfaces;

public interface ISensorDataRepo
{
    public Task<SensorData> GetAsync(string deviceId, DateTime timestamp);
    public Task<IEnumerable<SensorData>> GetAllAsync(Guid deviceId, DateTime start, DateTime end);
    public Task AddAsync(SensorData sensorData);
    public Task DeleteAsync(string deviceId, DateTime timestamp);
    
}