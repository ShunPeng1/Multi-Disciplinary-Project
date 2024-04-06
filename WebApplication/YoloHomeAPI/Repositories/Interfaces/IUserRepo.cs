using YoloHomeAPI.Data;
namespace YoloHomeAPI.Repositories.Interfaces;

public interface IUserRepo
{
    public Task<List<UserAuthenticationData>> GetAllAsync();
    public Task<UserAuthenticationData?> GetByUserAsync(string username);
    
    public Task<UserData?> GetByUserInformationAsync(string username);
    
    public Task AddAsync(UserAuthenticationData userAuthenticationData);
    public Task DeleteAsync(string username);
    public Task UpdateUserPasswordAsync(UserAuthenticationData userAuthenticationData);
    
    public Task UpdateUserInformationAsync(UserData userData);

}
