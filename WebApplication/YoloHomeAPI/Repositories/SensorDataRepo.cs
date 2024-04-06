using Npgsql;
using YoloHomeAPI.Data;
using YoloHomeAPI.Repositories.Interfaces;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Repositories;

public class SensorDataRepo : ISensorDataRepo
{
    private readonly DatabaseSettings _settings;
    private readonly string _connectionString;

    public SensorDataRepo(DatabaseSettings settings)
    {
        _settings = settings;
        _connectionString = settings.ConnectionString;
    }

    public async Task<SensorData> GetAsync(string deviceId, DateTime timestamp)
    {
        var query = "SELECT * FROM sensor_data WHERE s_did_fk = @deviceId AND s_timestmp = @timestamp";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("deviceId", Guid.Parse(deviceId));
        cmd.Parameters.AddWithValue("timestamp", timestamp);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new SensorData
            {
                TimeStamp = reader.GetDateTime(0),
                Value = reader.GetString(1),
                DeviceId = reader.GetGuid(2)
            };
        }
        return null!;
    }
    
    public async Task<IEnumerable<SensorData>> GetAllAsync(Guid deviceId, DateTime start, DateTime end)
    {
        var query = "SELECT * FROM sensor_data WHERE s_did_fk = @deviceId AND s_timestmp BETWEEN @start AND @end";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("deviceId", deviceId);
        cmd.Parameters.AddWithValue("start", start);
        cmd.Parameters.AddWithValue("end", end);
        await using var reader = await cmd.ExecuteReaderAsync();
        var sensorDataList = new List<SensorData>();
        while (await reader.ReadAsync())
        {
            sensorDataList.Add(new SensorData
            {
                TimeStamp = reader.GetDateTime(0),
                Value = reader.GetString(1),
                DeviceId = reader.GetGuid(2)
            });
        }
        return sensorDataList;
    }

    public async Task AddAsync(SensorData sensorData)
    {
        var query = "INSERT INTO sensor_data (s_timestmp, s_value, s_did_fk) VALUES (@timestamp, @value, @deviceId)";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("timestamp", sensorData.TimeStamp);
        cmd.Parameters.AddWithValue("value", sensorData.Value);
        cmd.Parameters.AddWithValue("deviceId", sensorData.DeviceId);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(string deviceId, DateTime timestamp)
    {
        var query = "DELETE FROM sensor_data WHERE s_did_fk = @deviceId AND s_timestmp = @timestamp";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("deviceId", Guid.Parse(deviceId));
        cmd.Parameters.AddWithValue("timestamp", timestamp);
        await cmd.ExecuteNonQueryAsync();
    }
}