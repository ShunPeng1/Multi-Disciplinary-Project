using YoloHomeAPI.Data;
using YoloHomeAPI.Repositories.Interfaces;
using Npgsql;

namespace YoloHomeAPI.Repositories;

public class UserRepo : IUserRepo
{
    private readonly string _connectionString;

    public UserRepo()
    {
        var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");
        var configuration = builder.Build();
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
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

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var query = "SELECT * FROM users WHERE username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", username);
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new User 
            { 
                UserName = reader.GetString(0), 
                PasswordHash = reader.GetString(1),
                PasswordSalt = reader.GetString(2)
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

    public async Task UpdateAsync(User user)
    {
        var query = "UPDATE users SET u_password = @password, u_salt = @salt WHERE u_username = @username";
        await using var dataSource = NpgsqlDataSource.Create(_connectionString);
        await using var cmd = dataSource.CreateCommand(query);
        cmd.Parameters.AddWithValue("username", user.UserName);
        cmd.Parameters.AddWithValue("password", user.PasswordHash);
        cmd.Parameters.AddWithValue("salt", user.PasswordSalt);
        await cmd.ExecuteNonQueryAsync();
    }
}
