using YoloHomeAPI.Services.Interfaces;

namespace YoloHomeAPI.Controllers;

using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class AuthenticationApiController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserService _userService;
    private readonly IIotDeviceService _iotDeviceService;
    
    public AuthenticationApiController(IAuthenticationService authenticationService, IUserService userService, IIotDeviceService iotDeviceService)
    {
        _authenticationService = authenticationService;
        _userService = userService;
        _iotDeviceService = iotDeviceService;
    }
    
    public class AuthenticationLoginRequest
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class AuthenticationRegisterRequest : AuthenticationLoginRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        
    }
    
    public class AuthenticationResponse
    {
        public string UserName { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
    
    [Route("Login")]
    [HttpPost]
    public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationLoginRequest authenticationLoginRequest)
    {
        var result = await _authenticationService.Login(authenticationLoginRequest.UserName, authenticationLoginRequest.Password);
        if (result.IsSuccess)
        {
            var response = new AuthenticationResponse()
            {
                UserName = authenticationLoginRequest.UserName,
                Token = result.Token
            };
            
            return Ok(response);
        }
        else
        {
            return BadRequest(result);
        }
    }
    
    
    [Route("Register")]
    [HttpPost]
    public async Task<ActionResult<AuthenticationResponse>> Register(AuthenticationRegisterRequest authenticationLoginRequest)
    {
        var result = await _authenticationService.Register(authenticationLoginRequest.UserName, authenticationLoginRequest.Password);
        if (result.IsSuccess)
        {
            var updateInformationResult = await _userService.UpdateUserInformation(authenticationLoginRequest.UserName, authenticationLoginRequest.FirstName, authenticationLoginRequest.LastName, authenticationLoginRequest.Email);
            var addDeviceResult = await _iotDeviceService.AddOwner(authenticationLoginRequest.UserName);
            return await Login(authenticationLoginRequest);
        }
        else
        {
            return BadRequest(result);
        }
    }
    
}
