using YoloHomeAPI.Services.Interfaces;

namespace YoloHomeAPI.Controllers;

using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class AuthenticationApiController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    
    public AuthenticationApiController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
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
            return await Login(authenticationLoginRequest);
        }
        else
        {
            return BadRequest(result);
        }
    }
    
}
