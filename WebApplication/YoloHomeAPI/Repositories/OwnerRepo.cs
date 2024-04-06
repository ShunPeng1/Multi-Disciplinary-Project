using Npgsql;
using YoloHomeAPI.Data;
using YoloHomeAPI.Repositories.Interfaces;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Repositories;

public class OwnerRepo : IOwnerRepo
{
    private readonly DatabaseSettings _settings;
    private readonly string _connectionString;

    public OwnerRepo(DatabaseSettings settings)
    {
        _settings = settings;
        _connectionString = settings.ConnectionString;
    }

    public async Task<IEnumerable<OwnerData>> GetAllAsync()
    {
        var query = "SELECT * FROM controls";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        await using var reader = await cmd.ExecuteReaderAsync();
        var owners = new List<OwnerData>();
        while (await reader.ReadAsync())
        {
            owners.Add(new OwnerData
            {
                UserName = reader.GetString(0),
                DeviceId = reader.GetGuid(1),
                Recurrence = reader.GetString(2),
                StartTime = reader.GetDateTime(3),
                EndTime = reader.GetDateTime(4),
            });
        }
        return owners;
    }

    public async Task<OwnerData> GetAsync(string username)
    {
        var query = "SELECT * FROM controls WHERE c_username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", username);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new OwnerData
            {
                UserName = reader.GetString(0),
                DeviceId = reader.GetGuid(1),
                Recurrence = reader.GetString(2),
                StartTime = reader.GetDateTime(3),
                EndTime = reader.GetDateTime(4),
            };
        }
        return null!;
    }

    public async Task AddAsync(OwnerData ownerData)
    {
        var query = "INSERT INTO controls (c_username, c_did, recurrence, start_time, end_time) VALUES (@username, @deviceId, @recurrence, @startTime, @endTime)";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", ownerData.UserName);
        cmd.Parameters.AddWithValue("deviceId", ownerData.DeviceId);
        cmd.Parameters.AddWithValue("recurrence", ownerData.Recurrence);
        cmd.Parameters.AddWithValue("startTime", ownerData.StartTime);
        cmd.Parameters.AddWithValue("endTime", ownerData.EndTime);
        
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(string username)
    {
        var query = "DELETE FROM controls WHERE c_username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", username);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(OwnerData ownerData)
    {
        var query = "UPDATE controls SET c_did = @deviceId, recurrence = @recurrence, start_time = @startTime, end_time = @endTime WHERE c_username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", ownerData.UserName);
        cmd.Parameters.AddWithValue("deviceId", ownerData.DeviceId);
        cmd.Parameters.AddWithValue("recurrence", ownerData.Recurrence);
        cmd.Parameters.AddWithValue("startTime", ownerData.StartTime);
        cmd.Parameters.AddWithValue("endTime", ownerData.EndTime);
        
        await cmd.ExecuteNonQueryAsync();
    }
}