using YoloHomeAPI.Data;

namespace YoloHomeAPI.Services.Interfaces;

public interface IActivityLogService
{
    public class ActivityLogResult
    {
        public ActivityLogResult(bool isSuccess, List<ActivityLogData> response)
        {
            IsSuccess = isSuccess;
            Response = response;
        }

        public bool IsSuccess { get; set; }
        public List<ActivityLogData> Response { get; set; }
        
    }
    
    public Task<ActivityLogResult> GetAll(string username, DateTime start, DateTime end);
    public Task<ActivityLogResult> Add(string username, string activity, DateTime timestamp);
    public Task<ActivityLogResult> Delete(string username, DateTime timestamp);
    
}