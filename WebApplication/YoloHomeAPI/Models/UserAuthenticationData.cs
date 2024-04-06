namespace YoloHomeAPI.Data;

public class UserAuthenticationData
{
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
}
