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
    
    
    public IManualControlService.ManualControlResult Execute(string userName, string id, string command)
    {
        Console.WriteLine($"Executing command {command} on {id}");
        switch (id)
        {
            case "announce":
                try
                {
                    _adafruitMqttService.PublishMessage(_adafruitSettings.AnnounceTopicPath, command == "on" ? "1" : "0" );
                    _activityLogService.Add(userName, "Announce "+command+" on", DateTime.Now);
                }
                catch (Exception e)
                {
                    return new IManualControlService.ManualControlResult(false, "Invalid command");

                }
                    
                break;
            case "fan" when command == "on":
                _adafruitMqttService.PublishMessage("yolo-home/feeds/fan", "1");
                break;
            case "fan" when command == "off":
                _adafruitMqttService.PublishMessage("yolo-home/feeds/fan", "0");
                break;
            case "fan":
                return new IManualControlService.ManualControlResult(false, "Invalid command");
            default:
                return new IManualControlService.ManualControlResult(false, "Invalid id");
        }
        
        
        
        return new IManualControlService.ManualControlResult(true, "Success");
        
    }
}