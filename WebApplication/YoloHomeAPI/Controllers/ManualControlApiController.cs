using Microsoft.AspNetCore.Mvc;

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
        public string Command { get; set; } = null!;
    }
    
    public class ManualControlResponse
    {
        public string Response { get; set; } = null!;
    }
    
    [Route("api/ManualControlApi/Execute")]
    [HttpPost]
    public ActionResult<ManualControlResponse> Execute(ManualControlRequest manualControlRequest)
    {
        var result = _manualControlService.Execute(manualControlRequest.Command);
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