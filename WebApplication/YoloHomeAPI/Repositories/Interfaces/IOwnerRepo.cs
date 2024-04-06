using YoloHomeAPI.Data;

namespace YoloHomeAPI.Repositories.Interfaces;

public interface IOwnerRepo
{
    
    public Task<IEnumerable<OwnerData>> GetAllAsync();
    public Task<OwnerData> GetAsync(string username);
    public Task AddAsync(OwnerData ownerData);
    public Task DeleteAsync(string username);
    public Task UpdateAsync(OwnerData ownerData);
    
    
}