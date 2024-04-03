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
    
    public class AuthenticationRequest
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    
    public class AuthenticationResponse
    {
        public string Token { get; set; } = null!;
    }
    
    [Route("Login")]
    [HttpPost]
    public ActionResult<AuthenticationResponse> Login(AuthenticationRequest authenticationRequest)
    {
        var result = _authenticationService.Login(authenticationRequest.UserName, authenticationRequest.Password);
        if (result.IsSuccess)
        {
            return Ok(result.Token);
        }
        else
        {
            return BadRequest(result);
        }
    }
    
    
    [Route("Register")]
    [HttpPost]
    public ActionResult<AuthenticationResponse> Register(AuthenticationRequest authenticationRequest)
    {
        var result = _authenticationService.Register(authenticationRequest.UserName, authenticationRequest.Password);
        if (result.IsSuccess)
        {
            return Login(authenticationRequest);
        }
        else
        {
            return BadRequest(result);
        }
    }
    
}
