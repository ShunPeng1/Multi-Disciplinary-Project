using Microsoft.AspNetCore.Mvc;
using YoloHomeAPI.Data;

namespace YoloHomeAPI.Controllers;

[ApiController]
public class UserApiController : ControllerBase
{
    [HttpGet("/api/user")]
    public string GetUser()
    {
        return "User data";
    }
    
}