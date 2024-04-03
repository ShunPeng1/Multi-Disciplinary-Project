using Microsoft.AspNetCore.Mvc;
using YoloHomeAPI.Services.Interfaces;

namespace YoloHomeAPI.Controllers;

[Route("api/ManualControlApi")]
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
        public string Id { get; set; } = null!;
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
        var result = _manualControlService.Execute(manualControlRequest.Id, manualControlRequest.Command);
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