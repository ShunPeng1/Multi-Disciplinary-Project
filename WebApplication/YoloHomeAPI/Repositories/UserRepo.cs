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

    public async Task<List<User>> GetAllAsync()
    {
        var query = "SELECT * FROM users";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        await using var reader = await cmd.ExecuteReaderAsync();
        var users = new List<User>();
        while (await reader.ReadAsync())
        {
            users.Add(new User 
            { 
                UserName = reader.GetString(0), 
                PasswordHash = reader.GetString(1),
                PasswordSalt = reader.GetString(5)
            });
        }
        return users;
    }
    
    public async Task<User?> GetByUserAsync(string username)
    {
        var query = "SELECT * FROM users WHERE username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", username);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new User() 
            { 
                UserName = reader.GetString(0),
                PasswordHash = reader.GetString(1),
                PasswordSalt = reader.GetString(2)
            };
        }
        return null;
    }

    public async Task<UserInformation?> GetByUserInformationAsync(string username)
    {
        var query = "SELECT * FROM users WHERE username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", username);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new UserInformation() 
            { 
                UserName = reader.GetString(0),
                Email = reader.GetString(3), 
                FirstName = reader.GetString(4),
                LastName = reader.GetString(5),
            };
        }
        return null;
    }

    public async Task AddAsync(User user)
    {
        var query = "INSERT INTO users (username, u_password, u_salt) VALUES (@username, @password, @salt)";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", user.UserName);
        cmd.Parameters.AddWithValue("password", user.PasswordHash);
        cmd.Parameters.AddWithValue("salt", user.PasswordSalt);
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

    public async Task UpdateUserPasswordAsync(User user)
    {
        var query = "UPDATE users SET u_password = @password, u_salt = @salt WHERE u_username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", user.UserName);
        cmd.Parameters.AddWithValue("password", user.PasswordHash);
        cmd.Parameters.AddWithValue("salt", user.PasswordSalt);
        await cmd.ExecuteNonQueryAsync();
    }
    
    public async Task UpdateUserInformationAsync(UserInformation userInformation)
    {
        var query = "UPDATE users SET fname = @firstname, lname = @lastname, email = @email WHERE username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", userInformation.UserName);
        cmd.Parameters.AddWithValue("firstname", userInformation.FirstName);
        cmd.Parameters.AddWithValue("lastname", userInformation.LastName);
        cmd.Parameters.AddWithValue("email", userInformation.Email);
        await cmd.ExecuteNonQueryAsync();
    }
}
