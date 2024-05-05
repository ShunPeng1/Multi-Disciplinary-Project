using YoloHomeAPI.Controllers;
using YoloHomeAPI.Services.Interfaces;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Services;

public interface ICommand
{
    string Execute();
}

// Concrete commands for different kinds of control
public class LightControlCommand : ICommand
{
    private readonly IAdafruitMqttService _adafruitMqttService;
    private readonly AdafruitSettings _adafruitSettings;
    private readonly IActivityLogService _activityLogService;
    private readonly string _userName;
    private readonly string _command;
    private readonly string _topicPath;

    public LightControlCommand(IAdafruitMqttService adafruitMqttService, AdafruitSettings adafruitSettings,
        IActivityLogService activityLogService, string userName, string command, string topicPath)
    {
        _adafruitMqttService = adafruitMqttService;
        _adafruitSettings = adafruitSettings;
        _activityLogService = activityLogService;
        _userName = userName;
        _command = command;
        _topicPath = topicPath;
    }

    public string Execute()
    {
        try
        {
            var result = _command == "On" ? "1" : "0";
            _adafruitMqttService.PublishMessage(_topicPath, result);
            _activityLogService.Add(_userName, $"Turn {_command.ToLower()} the Light", DateTime.Now);
            Console.WriteLine($"Executed command {_command} on Light");
            return result;
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid command");
            return "Invalid command";
        }
    }
}

public class FanControlCommand : ICommand
{
    private readonly IAdafruitMqttService _adafruitMqttService;
    private readonly AdafruitSettings _adafruitSettings;
    private readonly IActivityLogService _activityLogService;
    private readonly string _userName;
    private readonly string _command;
    private readonly string _topicPath;

    public FanControlCommand(IAdafruitMqttService adafruitMqttService, AdafruitSettings adafruitSettings,
        IActivityLogService activityLogService, string userName, string command, string topicPath)
    {
        _adafruitMqttService = adafruitMqttService;
        _adafruitSettings = adafruitSettings;
        _activityLogService = activityLogService;
        _userName = userName;
        _command = command;
        _topicPath = topicPath;
    }

    public string Execute()
    {
        try
        {
            _adafruitMqttService.PublishMessage(_topicPath, _command);
            _activityLogService.Add(_userName, $"Set speed to {_command.ToLower()} the Fan", DateTime.Now);
            Console.WriteLine($"Executed command {_command} on Fan");
            return _command;
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid command");
            return "Invalid command";
        }
    }
}

public class DoorControlCommand : ICommand
{
    private readonly IAdafruitMqttService _adafruitMqttService;
    private readonly AdafruitSettings _adafruitSettings;
    private readonly IActivityLogService _activityLogService;
    private readonly string _userName;
    private readonly string _command;
    private readonly string _topicPath;

    public DoorControlCommand(IAdafruitMqttService adafruitMqttService, AdafruitSettings adafruitSettings,
        IActivityLogService activityLogService, string userName, string command, string topicPath)
    {
        _adafruitMqttService = adafruitMqttService;
        _adafruitSettings = adafruitSettings;
        _activityLogService = activityLogService;
        _userName = userName;
        _command = command;
        _topicPath = topicPath;
    }

    public string Execute()
    {
        try
        {
            var result = _command == "Open" ? "1" : "0";
            _adafruitMqttService.PublishMessage(_topicPath, result);
            _activityLogService.Add(_userName, $"{_command} the Door", DateTime.Now);
            Console.WriteLine($"Executed command {_command} on Door");
            return result;
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid command");
            return "Invalid command";
        }
    }
}

// Factory interface
public interface ICommandFactory
{
    ICommand CreateCommand(string kind);
}

// Concrete Factory
public class CommandFactory : ICommandFactory
{
    private readonly IAdafruitMqttService _adafruitMqttService;
    private readonly AdafruitSettings _adafruitSettings;
    private readonly IActivityLogService _activityLogService;
    private readonly string _userName;
    private readonly string _command;


    public CommandFactory(IAdafruitMqttService adafruitMqttService, AdafruitSettings adafruitSettings,
        IActivityLogService activityLogService, string userName, string command)
    {
        _adafruitMqttService = adafruitMqttService;
        _adafruitSettings = adafruitSettings;
        _activityLogService = activityLogService;
        _userName = userName;
        _command = command;
    }
    
    public ICommand CreateCommand(string kind)
    {
        return kind switch
        {
            "Light" => new LightControlCommand(_adafruitMqttService, _adafruitSettings, _activityLogService,
                _userName, _command, _adafruitSettings.LightTopicPath),
            "Light2" => new LightControlCommand(_adafruitMqttService, _adafruitSettings, _activityLogService,
                _userName, _command, _adafruitSettings.Light2TopicPath),
            "Light3" => new LightControlCommand(_adafruitMqttService, _adafruitSettings, _activityLogService,
                _userName, _command, _adafruitSettings.Light3TopicPath),
            "Light4" => new LightControlCommand(_adafruitMqttService, _adafruitSettings, _activityLogService,
                _userName, _command, _adafruitSettings.Light4TopicPath),
            "Fan" => new FanControlCommand(_adafruitMqttService, _adafruitSettings, _activityLogService,
                _userName, _command, _adafruitSettings.FanTopicPath),
            "Door" => new DoorControlCommand(_adafruitMqttService, _adafruitSettings, _activityLogService,
                _userName, _command, _adafruitSettings.DoorTopicPath),
            
            // Add more cases for different kinds of controls as needed
            _ => throw new ArgumentException($"Unknown kind: {kind}")
        };
    }
    
}

// Invoker
public class ManualControlInvoker
{
    private readonly ICommand _command;

    public ManualControlInvoker(ICommand command)
    {
        _command = command;
    }
    public string ExecuteCommand()
    {
        return _command.Execute();
    }
}

public class ManualControlService : IManualControlService
{
    private readonly AdafruitSettings _adafruitSettings;
    private readonly IAdafruitMqttService _adafruitMqttService;
    private readonly IActivityLogService _activityLogService;

    public ManualControlService(AdafruitSettings adafruitSettings, IAdafruitMqttService adafruitMqttService,
        IActivityLogService activityLogService)
    {
        _adafruitSettings = adafruitSettings;
        _adafruitMqttService = adafruitMqttService;
        _activityLogService = activityLogService;
    }

    public IManualControlService.ManualControlResult Execute(string userName, string kind, string command)
    {
        try
        {
            // Create factory and command
            var factory = new CommandFactory(_adafruitMqttService, _adafruitSettings, _activityLogService, userName, command);
            var commandObj = factory.CreateCommand(kind);
            
            // Create invoker and execute the command
            var invoker = new ManualControlInvoker(commandObj);
            var result = invoker.ExecuteCommand();

            // Return the result
            return new IManualControlService.ManualControlResult(true, result);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new IManualControlService.ManualControlResult(false, e.Message);
        }
    }
}