using System.Diagnostics;
using YoloHomeAPI.Data;

namespace YoloHomeAPI.Repositories.Interfaces;

public interface IActivityLogRepo
{
    public Task<IEnumerable<ActivityLogData>> GetAllAsync(string username, DateTime start, DateTime end);
    public Task AddAsync(ActivityLogData activityLogData);
    public Task DeleteAsync(string username, DateTime timestamp);
}
