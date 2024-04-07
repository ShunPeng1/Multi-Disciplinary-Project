using Microsoft.AspNetCore.Mvc;
using YoloHomeAPI.Services.Interfaces;

namespace YoloHomeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManualControlApiController : ControllerBase
{
    private readonly IManualControlService _manualControlService;
    
    public ManualControlApiController(IManualControlService manualControlService)
    {
        _manualControlService = manualControlService;
    }
    
    public class ManualControlRequest
    {
        public string UserName { get; set; } = null!;
        public string Kind { get; set; } = null!;
        public string Command { get; set; } = null!;
    }
    
    public class ManualControlResponse
    {
        public string Response { get; set; } = null!;
    }
    
    [Route("Control")]
    [HttpPost]
    public ActionResult<ManualControlResponse> Control(ManualControlRequest manualControlRequest)
    {
        var result = _manualControlService.Execute(manualControlRequest.UserName ,manualControlRequest.Kind, manualControlRequest.Command);
        if (result.IsSuccess)
        {
            return Ok(new ManualControlResponse()
            {
                Response = result.Response
            });
        }
        else
        {
            return BadRequest(result);
        }
    }
    
}