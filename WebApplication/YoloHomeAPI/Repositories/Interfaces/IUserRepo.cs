using YoloHomeAPI.Data;
namespace YoloHomeAPI.Repositories.Interfaces;

public interface IUserRepo
{
    public Task<List<User>> GetAllAsync();
    public Task<User?> GetByUsernameAsync(string username);
    public Task AddAsync(User user);
    public Task DeleteAsync(string username);
    public Task UpdateAsync(User user);

}
