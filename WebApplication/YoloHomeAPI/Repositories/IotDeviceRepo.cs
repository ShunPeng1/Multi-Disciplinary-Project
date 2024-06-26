﻿using Npgsql;
using YoloHomeAPI.Data;
using YoloHomeAPI.Repositories.Interfaces;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Repositories;

public class IotDeviceRepo : IIotDeviceRepo
{
   
    private readonly DatabaseSettings _settings;
    private readonly string _connectionString;

    public IotDeviceRepo(DatabaseSettings settings)
    {
        _settings = settings;
        _connectionString = settings.ConnectionString;
    }
    
    public async Task<IEnumerable<IotDeviceData>> GetAllAsync()
    {
        var query = "SELECT * FROM device";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        await using var reader = await cmd.ExecuteReaderAsync();
        var devices = new List<IotDeviceData>();
        while (await reader.ReadAsync())
        {
            devices.Add(new IotDeviceData
            {
                DeviceId = reader.GetGuid(0),
                DeviceName = reader.GetString(1),
                DeviceLocation = reader.GetString(2),
                DeviceType = reader.GetString(3),
                DeviceState = reader.GetString(4)
            });
        }
        return devices;
    }

    public async Task<IEnumerable<IotDeviceData>> GetAllByUserNameAsync(string username)
    {
        var query = "SELECT d.* FROM device d JOIN controls c ON d.device_id = c.c_did WHERE c.c_username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", username);
        await using var reader = await cmd.ExecuteReaderAsync();
        var devices = new List<IotDeviceData>();
        while (await reader.ReadAsync())
        {
            devices.Add(new IotDeviceData
            {
                DeviceId = reader.GetGuid(0),
                DeviceName = reader.GetString(1),
                DeviceLocation = reader.GetString(2),
                DeviceType = reader.GetString(3),
                DeviceState = reader.GetString(4)
            });
        }
        return devices;
    }

    
    public async Task AddAsync(IotDeviceData iotDeviceData)
    {
        var query = "INSERT INTO device (device_id, d_name, d_location, d_type, d_state) VALUES (@deviceId, @name, @location, @type, @state)";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("deviceId", iotDeviceData.DeviceId);
        cmd.Parameters.AddWithValue("name", iotDeviceData.DeviceName);
        cmd.Parameters.AddWithValue("location", iotDeviceData.DeviceLocation);
        cmd.Parameters.AddWithValue("type", iotDeviceData.DeviceType);
        cmd.Parameters.AddWithValue("state", iotDeviceData.DeviceState);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(Guid deviceId)
    {
        var query = "DELETE FROM device WHERE device_id = @deviceId";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        
        cmd.Parameters.AddWithValue("deviceId", deviceId);
        await cmd.ExecuteNonQueryAsync();
    }
}