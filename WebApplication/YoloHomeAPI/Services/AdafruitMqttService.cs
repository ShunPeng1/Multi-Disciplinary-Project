using System.Text;
using MQTTnet;
using MQTTnet.Client;
using YoloHomeAPI.Controllers;
using YoloHomeAPI.Data;
using YoloHomeAPI.Services.Interfaces;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Services;



public class AdafruitMqttService : BackgroundService, IAdafruitMqttService
{
    private const float SEND_DATA_BACK_TIMER = 120f;

    private readonly AdafruitSettings _adafruitSetting;

    private IMqttClient _mqttClient;

    private MqttClientOptions _mqttClientOptions;
    
    private IIotDeviceService _iotDeviceService;


    public Action<AdafruitDataReceiveData> OnDoorMessageReceived { get; set; } = delegate{ };
    public Action<AdafruitDataReceiveData> OnFanReceived { get; set; } = delegate{ };
    public Action<AdafruitDataReceiveData> OnLightMessageReceived { get; set; } = delegate{ };
    public Action<AdafruitDataReceiveData> OnHumidityMessageReceived { get; set; } = delegate { };
    public Action<AdafruitDataReceiveData> OnTemperatureMessageReceived { get; set; } = delegate { };
    
    public AdafruitMqttService(AdafruitSettings adafruitSetting, IMqttClient mqttClient, IIotDeviceService iotDeviceService)
    {
        _adafruitSetting = adafruitSetting;
        
        _mqttClient = mqttClient;
        _iotDeviceService = iotDeviceService;

        _mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("io.adafruit.com", 8883)
            .WithCredentials(_adafruitSetting.AdafruitUsername, _adafruitSetting.AdafruitKey)
            .WithTls()
            .Build();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting AdafruitMqttService...");
        
        await base.StartAsync(cancellationToken);
        
        
        var mqttFactory = new MqttFactory();
        _mqttClient = mqttFactory.CreateMqttClient();

        Console.WriteLine($"Initializing AdafruitMqttService...  {_adafruitSetting.AdafruitUsername}, {_adafruitSetting.AdafruitKey}");
        
        
        _mqttClient.ApplicationMessageReceivedAsync += ApplicationMessageReceivedHandler;
        await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);

        var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
            .WithTopicFilter(f => { f.WithTopic(_adafruitSetting.LightTopicPath); })
            .WithTopicFilter(f => { f.WithTopic(_adafruitSetting.Light2TopicPath); })
            .WithTopicFilter(f => { f.WithTopic(_adafruitSetting.Light3TopicPath); })
            .WithTopicFilter(f => { f.WithTopic(_adafruitSetting.Light4TopicPath); })
            .WithTopicFilter(f => { f.WithTopic(_adafruitSetting.FanTopicPath); })
            .WithTopicFilter(f => { f.WithTopic(_adafruitSetting.DoorTopicPath); })
            .WithTopicFilter(f => { f.WithTopic(_adafruitSetting.HumidityTopicPath); })
            .WithTopicFilter(f => { f.WithTopic(_adafruitSetting.TemperatureTopicPath); })
            .Build();

        await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        
        Console.WriteLine("AdafruitMqttService initialized successfully.");
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping AdafruitMqttService...");
        
        await base.StopAsync(cancellationToken);
        await _mqttClient.DisconnectAsync();
        
        
        Console.WriteLine("AdafruitMqttService stopped successfully.");
    }

    public async void PublishMessage(string topic, string content)
    {
        if (!_mqttClient.IsConnected)
        {
            Console.WriteLine("Error: MQTT client is not connected.");
            try
            {
                await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
                Console.WriteLine("MQTT client reconnected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Failed to reconnect MQTT client: {ex.Message}");
                return;
            }
        }
        
        Console.WriteLine("Publishing message to topic: " + topic + " with content: " + content);
        var applicationMessage = new MqttApplicationMessageBuilder().WithTopic(topic).WithPayload(content).Build();
        await _mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

        Console.WriteLine("Message published.");
    
    }

    private Task ApplicationMessageReceivedHandler(MqttApplicationMessageReceivedEventArgs args)
    {
        var decodedMessage = DecodeMqttPayload(args.ApplicationMessage.Payload);
        var topic = args.ApplicationMessage.Topic;
        
        AdafruitDataReceiveData adafruitDataReceiveData = new()
        {
            Topic = topic,
            RawMessage = decodedMessage,
            Values = decodedMessage.Split(';').Select(float.Parse).ToList() 
        };


        _iotDeviceService.AddSensorDataAsync(adafruitDataReceiveData);

        
        Console.WriteLine("Message received: " + decodedMessage);
        return Task.CompletedTask;
    }
    
    private string DecodeMqttPayload(byte[] encodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(Convert.ToBase64String(encodedData));
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

}