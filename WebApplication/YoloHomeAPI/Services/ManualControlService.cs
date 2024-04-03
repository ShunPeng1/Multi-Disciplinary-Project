using YoloHomeAPI.Controllers;
using YoloHomeAPI.Services.Interfaces;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Services;

public class ManualControlService : IManualControlService
{
    private readonly IAdafruitMqttService _adafruitMqttService;
    private readonly AdafruitSettings _adafruitSettings;
    
    public ManualControlService(AdafruitSettings adafruitSettings, IAdafruitMqttService adafruitMqttService)
    {
        _adafruitSettings = adafruitSettings;
        _adafruitMqttService = adafruitMqttService;
    }
    
    
    public IManualControlService.ManualControlResult Execute(string id, string command)
    {
        Console.WriteLine($"Executing command {command} on {id}");
        if (id == "announce")
        {
            if (command == "on")
            {
                _adafruitMqttService.PublishMessage(_adafruitSettings.AnnounceTopicPath, "1");
            }
            else if (command == "off")
            {
                _adafruitMqttService.PublishMessage(_adafruitSettings.AnnounceTopicPath, "0");
            }
            else
            {
                return new IManualControlService.ManualControlResult(false, "Invalid command");
            }
        }
        else if (id == "fan")
        {
            if (command == "on")
            {
                _adafruitMqttService.PublishMessage("yolo-home/feeds/fan", "1");
            }
            else if (command == "off")
            {
                _adafruitMqttService.PublishMessage("yolo-home/feeds/fan", "0");
            }
            else
            {
                return new IManualControlService.ManualControlResult(false, "Invalid command");
            }
        }
        else
        {
            return new IManualControlService.ManualControlResult(false, "Invalid id");
        }
        
        
        
        return new IManualControlService.ManualControlResult(true, "Success");
        
    }
}