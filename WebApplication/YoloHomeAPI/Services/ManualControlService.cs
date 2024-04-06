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
                    _adafruitMqttService.PublishMessage(_adafruitSettings.LightTopicPath, command == "On" ? "1" : "0" );
                    _activityLogService.Add(userName, "Light is turned "+command, DateTime.Now);
                    return new IManualControlService.ManualControlResult(true, "Success");
                }
                catch (Exception e)
                {
                    return new IManualControlService.ManualControlResult(false, "Invalid command");

                }
                break;
            
            case "Fan":
                try
                {
                    _adafruitMqttService.PublishMessage(_adafruitSettings.FanTopicPath, command == "On" ? "1" : "0" );
                    _activityLogService.Add(userName, "Fan is turned "+command, DateTime.Now);
                    return new IManualControlService.ManualControlResult(true, "Success");
                }
                catch (Exception e)
                {
                    return new IManualControlService.ManualControlResult(false, "Invalid command");

                }
                break;
            
            case "Door":
                try
                {
                    _adafruitMqttService.PublishMessage(_adafruitSettings.DoorTopicPath, command == "Open" ? "1" : "0" );
                    _activityLogService.Add(userName, "Door is "+command+"ed", DateTime.Now);
                    return new IManualControlService.ManualControlResult(true, "Success");
                }
                catch (Exception e)
                {
                    return new IManualControlService.ManualControlResult(false, "Invalid command");

                }
                break;
            
            default:
                return new IManualControlService.ManualControlResult(false, "Invalid kind");
            
        }
        
        
        
        return new IManualControlService.ManualControlResult(true, "Success");
        
    }
}