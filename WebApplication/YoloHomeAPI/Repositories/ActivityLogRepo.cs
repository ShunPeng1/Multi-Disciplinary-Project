﻿using YoloHomeAPI.Data;
using YoloHomeAPI.Repositories.Interfaces;
using Npgsql;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Repositories;

public class ActivityLogRepo : IActivityLogRepo
{
    private readonly DatabaseSettings _settings;
    private readonly string _connectionString;

    public ActivityLogRepo(DatabaseSettings settings)
    {
        _settings = settings;
        _connectionString = settings.ConnectionString;
    }

    
    public async Task<IEnumerable<ActivityLogData>> GetAllAsync(string username, DateTime start, DateTime end)
    {
        //var query = "SELECT * FROM log_record WHERE l_username_fk = @username AND l_timestmp >= @start AND l_timestmp <= @end";
        var query = "SELECT * FROM log_record WHERE l_timestmp >= @start AND l_timestmp <= @end";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        //cmd.Parameters.AddWithValue("username", username);
        cmd.Parameters.AddWithValue("start", start);
        cmd.Parameters.AddWithValue("end", end);
        await using var reader = await cmd.ExecuteReaderAsync();
        var activityLogs = new List<ActivityLogData>();
        while (await reader.ReadAsync())
        {
            activityLogs.Add(new ActivityLogData
            {
                TimeStamp = reader.GetDateTime(0),
                Activity = reader.GetString(1),
                UserName = reader.GetString(2)
            });
        }
        return activityLogs;
    }
    public async Task AddAsync(ActivityLogData activityLogData)
    {
        var query = "INSERT INTO log_record (l_timestmp, activity, l_username_fk) VALUES (@timestamp, @activity, @username)";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("timestamp", activityLogData.TimeStamp);
        cmd.Parameters.AddWithValue("username", activityLogData.UserName);
        cmd.Parameters.AddWithValue("activity", activityLogData.Activity);
        var rowsAffected = await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(string username, DateTime timestamp)
    {
        var query = "DELETE FROM log_record WHERE l_username_fk = @username AND l_timestmp = @timestamp";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("timestamp", timestamp);
        cmd.Parameters.AddWithValue("username", username);
        await cmd.ExecuteNonQueryAsync();
    }
}
