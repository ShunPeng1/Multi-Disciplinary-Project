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
    
    public ActivityLogResult GetAll(string username, DateTime start, DateTime end);
    public ActivityLogResult Add(string username, string activity, DateTime timestamp);
    public ActivityLogResult Delete(string username, DateTime timestamp);
    
}