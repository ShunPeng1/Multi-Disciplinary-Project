using YoloHomeAPI.Data;

namespace YoloHomeAPI.Repositories.Interfaces;

public interface ISensorDataRepo
{
    public Task<SensorData> GetAsync(Guid deviceId, DateTime timestamp);
    public Task<IEnumerable<SensorData>> GetAllAsync(Guid deviceId, DateTime start, DateTime end);
    public Task AddAsync(SensorData sensorData);
    public Task DeleteAsync(Guid deviceId, DateTime timestamp);
    
}