using YoloHomeAPI.Data;

namespace YoloHomeAPI.Services.Interfaces;

public interface IUserService
{
    public class UserResult
    {
        public UserResult(bool isSuccess , UserData response)
        {
            IsSuccess = isSuccess;
            Response = response;
        }

        public bool IsSuccess { get; set; }
        public UserData Response { get; set; }
    }
    
    public Task<UserResult> GetUserInformation(string username);
    
    public Task<UserResult> RemoveUser(string username);
    public Task<UserResult> UpdateUserPassword(string username, string password);
    public Task<UserResult> UpdateUserInformation(string username, string firstName, string lastName, string email);
    
    
}