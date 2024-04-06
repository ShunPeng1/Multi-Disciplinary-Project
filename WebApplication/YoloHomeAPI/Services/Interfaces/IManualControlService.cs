namespace YoloHomeAPI.Services.Interfaces;

public interface IManualControlService
{
    public class ManualControlResult
    {
        public ManualControlResult(bool isSuccess , string response)
        {
            IsSuccess = isSuccess;
            Response = response;
        }

        public bool IsSuccess { get; set; }
        public string Response { get; set; }
    }
    
    public ManualControlResult Execute(string userName, string kind, string command);
}