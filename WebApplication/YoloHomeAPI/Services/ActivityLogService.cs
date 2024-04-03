using YoloHomeAPI.Data;
using YoloHomeAPI.Services.Interfaces;

namespace YoloHomeAPI.Services;

public class ActivityLogService : IActivityLogService
{
    // In-memory data store TODO replace with database
    private readonly List<ActivityLogData> _activityLogData = new List<ActivityLogData>(); 
    
    public IActivityLogService.ActivityLogResult GetAll(string username, DateTime start, DateTime end)
    {
        var activityLogs = _activityLogData.FindAll(x => x.UserName == username);
        
        if (activityLogs.Count == 0)
        {
            return new IActivityLogService.ActivityLogResult(false, null!);
        }
        
        return new IActivityLogService.ActivityLogResult(true, activityLogs );
    }
    

    public IActivityLogService.ActivityLogResult Add(string username, string activity, DateTime timestamp)
    {
        var activityLog = new ActivityLogData()
        {
            UserName = username,
            Activity = activity,
            TimeStamp = timestamp
        };
        _activityLogData.Add(activityLog);
        return new IActivityLogService.ActivityLogResult(true, new List<ActivityLogData>(){activityLog} );
    }

    public IActivityLogService.ActivityLogResult Delete(string username, DateTime timestamp)
    {
        return new IActivityLogService.ActivityLogResult(true, null!);
    }

    
}