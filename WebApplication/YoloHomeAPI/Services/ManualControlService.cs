using YoloHomeAPI.Controllers;
using YoloHomeAPI.Services.Interfaces;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Services;

public class ManualControlService : IManualControlService
{
    private readonly AdafruitSettings _adafruitSettings;
    private readonly IAdafruitMqttService _adafruitMqttService;

    private readonly IActivityLogService _activityLogService;
    
    public ManualControlService(AdafruitSettings adafruitSettings, IAdafruitMqttService adafruitMqttService, IActivityLogService activityLogService)
    {
        _adafruitSettings = adafruitSettings;
        _adafruitMqttService = adafruitMqttService;
        _activityLogService = activityLogService;
    }
    
    
    public IManualControlService.ManualControlResult Execute(string userName, string kind, string command)
    {
        Console.WriteLine($"Executing command {command} on {kind}");
        switch (kind)
        {
            case "Light":
                try
                {
                    var result = command == "On" ? "1" : "0"; 
                    _adafruitMqttService.PublishMessage(_adafruitSettings.LightTopicPath, result);
                    _activityLogService.Add(userName, userName + " turns " + command + " the light.", DateTime.Now);
                    return new IManualControlService.ManualControlResult(true, result);
                }
                catch (Exception _)
                {
                    return new IManualControlService.ManualControlResult(false, "Invalid command");

                }
                
            case "Fan":
                try
                {
                    var result = command == "On" ? "1" : "0"; 
                    _adafruitMqttService.PublishMessage(_adafruitSettings.FanTopicPath, result );
                    _activityLogService.Add(userName, userName + " turns " + command + " the fan.", DateTime.Now);
                    return new IManualControlService.ManualControlResult(true, result);
                }
                catch (Exception _)
                {
                    return new IManualControlService.ManualControlResult(false, "Invalid command");

                }
            
            case "Door":
                try
                {
                    var result = command == "Open" ? "1" : "0"; 
                    _adafruitMqttService.PublishMessage(_adafruitSettings.DoorTopicPath, result);
                    _activityLogService.Add(userName, userName+" " + command + " the door.", DateTime.Now);
                    return new IManualControlService.ManualControlResult(true, result);
                }
                catch (Exception _)
                {
                    return new IManualControlService.ManualControlResult(false, "Invalid command");

                }
            
            default:
                return new IManualControlService.ManualControlResult(false, "Invalid kind");
            
        }
        
    }
}