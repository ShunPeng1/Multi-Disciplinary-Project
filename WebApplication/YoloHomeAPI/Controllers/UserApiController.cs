using Microsoft.AspNetCore.Mvc;
using YoloHomeAPI.Data;
using YoloHomeAPI.Services.Interfaces;

namespace YoloHomeAPI.Controllers;

[ApiController]
public class UserApiController : ControllerBase
{
    private readonly IUserService _userService;

    public UserApiController(IUserService userService)
    {
        _userService = userService;
    }

    public class UserRequest
    {
        public string Username { get; set; } = null!;
    }
    
    
    [Route("api/[controller]/GetUserInformation")]
    [HttpGet]
    public async Task<ActionResult<UserData>> GetUser([FromQuery] UserRequest userRequest)
    {
        var result = await _userService.GetUserInformation(userRequest.Username);
        
        if (result.IsSuccess)
        {
            return Ok(result.Response);
        }
        else
        {
            return BadRequest(result);
        }
    }
    
    
}