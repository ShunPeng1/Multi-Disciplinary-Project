using YoloHomeAPI.Data;
using YoloHomeAPI.Repositories.Interfaces;
using Npgsql;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Repositories;

public class UserRepo : IUserRepo
{
    private readonly DatabaseSettings _settings;
    private readonly string _connectionString;

    public UserRepo(DatabaseSettings settings)
    {
        _settings = settings;
        _connectionString = settings.ConnectionString;
    }

    public async Task<List<UserAuthenticationData>> GetAllAsync()
    {
        var query = "SELECT * FROM users";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        await using var reader = await cmd.ExecuteReaderAsync();
        var users = new List<UserAuthenticationData>();
        while (await reader.ReadAsync())
        {
            users.Add(new UserAuthenticationData 
            { 
                UserName = reader.GetString(0), 
                PasswordHash = reader.GetString(1),
                PasswordSalt = reader.GetString(5)
            });
        }
        return users;
    }
    
    public async Task<UserAuthenticationData?> GetByUserAsync(string username)
    {
        var query = "SELECT * FROM users WHERE username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", username);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new UserAuthenticationData() 
            { 
                UserName = reader.GetString(0),
                PasswordHash = reader.GetString(1),
                PasswordSalt = reader.GetString(2)
            };
        }
        return null;
    }

    public async Task<UserData?> GetByUserInformationAsync(string username)
    {
        var query = "SELECT * FROM users WHERE username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", username);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new UserData() 
            { 
                UserName = reader.GetString(0),
                Email = reader.GetString(3), 
                FirstName = reader.GetString(4),
                LastName = reader.GetString(5),
            };
        }
        return null;
    }

    public async Task AddAsync(UserAuthenticationData userAuthenticationData)
    {
        var query = "INSERT INTO users (username, u_password, u_salt) VALUES (@username, @password, @salt)";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", userAuthenticationData.UserName);
        cmd.Parameters.AddWithValue("password", userAuthenticationData.PasswordHash);
        cmd.Parameters.AddWithValue("salt", userAuthenticationData.PasswordSalt);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(string username)
    {
        var query = "DELETE FROM users WHERE u_username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", username);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task UpdateUserPasswordAsync(UserAuthenticationData userAuthenticationData)
    {
        var query = "UPDATE users SET u_password = @password, u_salt = @salt WHERE u_username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", userAuthenticationData.UserName);
        cmd.Parameters.AddWithValue("password", userAuthenticationData.PasswordHash);
        cmd.Parameters.AddWithValue("salt", userAuthenticationData.PasswordSalt);
        await cmd.ExecuteNonQueryAsync();
    }
    
    public async Task UpdateUserInformationAsync(UserData userData)
    {
        var query = "UPDATE users SET fname = @firstname, lname = @lastname, email = @email WHERE username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", userData.UserName);
        cmd.Parameters.AddWithValue("firstname", userData.FirstName);
        cmd.Parameters.AddWithValue("lastname", userData.LastName);
        cmd.Parameters.AddWithValue("email", userData.Email);
        await cmd.ExecuteNonQueryAsync();
    }
}
