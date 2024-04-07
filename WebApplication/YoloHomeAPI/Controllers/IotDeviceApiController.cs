﻿using Microsoft.AspNetCore.Mvc;
using YoloHomeAPI.Services.Interfaces;

namespace YoloHomeAPI.Controllers;

[Route("api/[controller]")]
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
    
    public class SensorDataRequest
    {
        public Guid DeviceId { get; set; } = Guid.Empty;
        
        public string DeviceType { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
    
    public class IotDeviceResponse
    {
        public string Response { get; set; } = null!;
    }
    
    public class SensorDataResponse
    {
        public string Response { get; set; } = null!;
    }
     
    [Route("GetAllDevices")]
    [HttpGet]
    public async Task<ActionResult<IotDeviceResponse>> GetAllDevices([FromQuery] IotDeviceRequest iotDeviceRequest)
    {
        var result = await _iotDeviceService.GetAllDevices(iotDeviceRequest.Username);
        if (result.IsSuccess)
        {
            return Ok(result.Response);
        }
        else
        {
            return BadRequest(result.Response);
        }
    }
    
    [Route("GetAllSensorData")]
    [HttpGet]
    public async Task<ActionResult<List<SensorDataResponse>>> GetAllSensorData([FromQuery] SensorDataRequest sensorDataRequest)
    {
        var result = await _iotDeviceService.GetAllSensorData(sensorDataRequest.DeviceId, sensorDataRequest.Start, sensorDataRequest.End);
        if (result.IsSuccess)
        {
            var sensorDataResponses = result.Response.Select(sensorData => new SensorDataResponse() { Response = sensorData.Value }).ToList();
            return Ok(sensorDataResponses);
        }
        else
        {
            return BadRequest(result.Response);
        }
    }
    
    
    [Route("GetLatestSensorData")]
    [HttpGet]
    public async Task<ActionResult<SensorDataResponse>> GetLatestSensorData([FromQuery] SensorDataRequest sensorDataRequest)
    {
        var result = await _iotDeviceService.GetLatestSensorData(sensorDataRequest.DeviceType);
        if (result.IsSuccess)
        {
            var sensorDataResponses = result.Response.Select(sensorData => new SensorDataResponse() { Response = sensorData.Value }).ToList();
            return Ok(sensorDataResponses.Count > 0 ? sensorDataResponses[0] : new SensorDataResponse() { Response = "No data found" });
        }
        else
        {
            return BadRequest(result.Response);
        }
    }
    
}