using Microsoft.AspNetCore.Mvc;
using YoloHomeAPI.Data;
using YoloHomeAPI.Services.Interfaces;

namespace YoloHomeAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivityLogApiController : ControllerBase   
{
    private IActivityLogService _activityLogService;
    
    public ActivityLogApiController(IActivityLogService activityLogService)
    {
        _activityLogService = activityLogService;
    }
    
    public class ActionLogRequest
    {
        public string Username { get; set; } = null!;
        public DateTime Start { get; set; } = DateTime.MinValue;
        public DateTime End { get; set; } = DateTime.MaxValue;
        
    }
    
    public class ActionLogResponse
    {
        public string Response { get; set; } = null!;
    }
    
    [Route("GetAll")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityLogData>>> GetAll([FromQuery] ActionLogRequest actionLogRequest)
    {
        
        var result = await _activityLogService.GetAll(actionLogRequest.Username, actionLogRequest.Start, actionLogRequest.End);
        
        if (result.IsSuccess)
        {
            return Ok(result.Response);
        }
        else
        {
            return BadRequest(result);
        }
        
    }
    
    [Route("Delete")]
    [HttpPost]
    public async Task<ActionResult<ActionLogResponse>> Delete(ActionLogRequest actionLogRequest)
    {
        var result = await _activityLogService.Delete(actionLogRequest.Username, actionLogRequest.Start);
        
        if (result.IsSuccess)
        {
            return Ok("Success");
        }
        else
        {
            return BadRequest(result);
        }
    }
    
}