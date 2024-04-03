using Microsoft.AspNetCore.Mvc;
using YoloHomeAPI.Services.Interfaces;

namespace YoloHomeAPI.Controllers;

public class IotDeviceApiController : ControllerBase
{
    private readonly IIotDeviceService _iotDeviceService;
    
    public IotDeviceApiController(IIotDeviceService iotDeviceService)
    {
        _iotDeviceService = iotDeviceService;
    }
    
    public class IotDeviceRequest
    {
        public string Username { get; set; } = null!;
        
    }
    
    public class IotDeviceResponse
    {
        public string Response { get; set; } = null!;
    }
    
    [Route("GetAll")]
    [HttpGet]
    public ActionResult<IotDeviceResponse> GetAll(IotDeviceRequest iotDeviceRequest)
    {
        var result = _iotDeviceService.GetAll(iotDeviceRequest.Username);
        if (result.IsSuccess)
        {
            return Ok(result.Response);
        }
        else
        {
            return BadRequest(result.Response);
        }
    }
    
    [Route("Get")]
    [HttpGet]
    public ActionResult<IotDeviceResponse> Get(IotDeviceRequest iotDeviceRequest)
    {
        var result = _iotDeviceService.Get(iotDeviceRequest.Username);
        if (result.IsSuccess)
        {
            return Ok(result.Response);
        }
        else
        {
            return BadRequest(result.Response);
        }
    }
    
    
    
}