using YoloHomeAPI.Data;
using YoloHomeAPI.Repositories;
using YoloHomeAPI.Repositories.Interfaces;
using YoloHomeAPI.Services.Interfaces;

namespace YoloHomeAPI.Services;

public class ActivityLogService : IActivityLogService
{
    private IActivityLogRepo _activityLogRepo;
    public ActivityLogService()
    {
        _activityLogRepo = new ActivityLogRepo();
    }
    public async Task<IActivityLogService.ActivityLogResult> GetAll(string username, DateTime start, DateTime end)
    {
        List<ActivityLogData> activityLogs;
        activityLogs = (await _activityLogRepo.GetAllAsync(username, start, end)).ToList();
        
        if (activityLogs.Count == 0)
        {
            return new IActivityLogService.ActivityLogResult(false, null!);
        }
        
        return new IActivityLogService.ActivityLogResult(true, activityLogs );
    }
    

    public async Task<IActivityLogService.ActivityLogResult> Add(string username, string activity, DateTime timestamp)
    {
        var activityLog = new ActivityLogData()
        {
            UserName = username,
            Activity = activity,
            TimeStamp = timestamp
        };
        await _activityLogRepo.AddAsync(activityLog);
        return new IActivityLogService.ActivityLogResult(true, new List<ActivityLogData>(){activityLog} );
    }

    public async Task<IActivityLogService.ActivityLogResult> Delete(string username, DateTime timestamp)
    {
        await _activityLogRepo.DeleteAsync(username, timestamp);
        return new IActivityLogService.ActivityLogResult(true, null!);
    }

    
}