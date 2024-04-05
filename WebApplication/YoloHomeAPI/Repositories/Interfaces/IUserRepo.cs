using YoloHomeAPI.Data;
namespace YoloHomeAPI.Repositories.Interfaces;

public interface IUserRepo
{
    public Task<List<User>> GetAllAsync();
    public Task<User?> GetByUserAsync(string username);
    
    public Task<UserInformation?> GetByUserInformationAsync(string username);
    
    public Task AddAsync(User user);
    public Task DeleteAsync(string username);
    public Task UpdateUserPasswordAsync(User user);
    
    public Task UpdateUserInformationAsync(UserInformation userInformation);

}
