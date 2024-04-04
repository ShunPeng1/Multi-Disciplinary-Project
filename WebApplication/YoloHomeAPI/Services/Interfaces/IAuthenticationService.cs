namespace YoloHomeAPI.Services.Interfaces;

public interface IAuthenticationService
{
    public class AuthenticationResult
    {
        public AuthenticationResult(bool isSuccess , string message, string token)
        {
            IsSuccess = isSuccess;
            Message = message;
            Token = token;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
    
    public Task<AuthenticationResult> Register(string username, string password);
    public Task<AuthenticationResult> Login(string username, string password);


}