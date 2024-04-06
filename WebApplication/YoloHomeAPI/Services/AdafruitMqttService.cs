using System.Text;
using MQTTnet;
using MQTTnet.Client;
using YoloHomeAPI.Controllers;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Services;



public class AdafruitMqttService : BackgroundService, IAdafruitMqttService
{
    private const float SEND_DATA_BACK_TIMER = 120f;

    private readonly AdafruitSettings _adafruitSetting;
    
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private IMqttClient _mqttClient;

    private string _accumulatorStartTimeString = DateTime.UtcNow.ToString();
    private MqttClientOptions _mqttClientOptions;


    public AdafruitMqttService(AdafruitSettings adafruitSetting, IServiceScopeFactory serviceScopeFactory, IMqttClient mqttClient)
    {
        _adafruitSetting = adafruitSetting;
        _serviceScopeFactory = serviceScopeFactory;
        _mqttClient = mqttClient;
        _mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("io.adafruit.com", 8883)
            .WithCredentials(_adafruitSetting.AdafruitUsername, _adafruitSetting.AdafruitKey)
            .WithTls()
            .Build();
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

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(SEND_DATA_BACK_TIMER));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            /*
            if (_accumulatedPlantDataLogs.Count == 0)
            {
                Console.WriteLine("WARNING: The server did not accumulate any log in the last timespan (" + _accumulatorStartTimeString + " to " + DateTime.Now + ").");
                continue;
            }

            
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();

            var averagedLightValue = _accumulatedPlantDataLogs.Average(log => log.AveragedLightValue);
            var averagedTemperatureValue = _accumulatedPlantDataLogs.Average(log => log.AveragedTemperatureValue);
            var averagedMoistureValue = _accumulatedPlantDataLogs.Average(log => log.AveragedMoistureValue);

            PublishMessage(_helperService.LightTopicPath, averagedLightValue.ToString("0.00"));
            PublishMessage(_helperService.TemperatureTopicPath, averagedTemperatureValue.ToString("0.00"));
            PublishMessage(_helperService.HumidityTopicPath, averagedMoistureValue.ToString("0.00"));

            foreach (var accumulatedPlantDataLog in _accumulatedPlantDataLogs)
            {
                var ownerPlant = dbContext.PlantInformations.FirstOrDefault(info => info.Kind == accumulatedPlantDataLog.PlantId);
                if (ownerPlant == null) continue;

                dbContext.PlantDataLogs.Add(new PlantDataLog()
                {
                    Timestamp = DateTime.UtcNow,
                    LightValue = accumulatedPlantDataLog.AveragedLightValue,
                    TemperatureValue = accumulatedPlantDataLog.AveragedTemperatureValue,
                    MoistureValue = accumulatedPlantDataLog.AveragedMoistureValue,
                    LoggedPlant = ownerPlant,
                });
            }

            await dbContext.SaveChangesAsync(stoppingToken);

            _accumulatedPlantDataLogs = new();
            _accumulatorStartTimeString = DateTime.UtcNow.ToString();
            */
        }
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
        Console.WriteLine("From topic: " + args.ApplicationMessage.Topic);

        if (args.ApplicationMessage.Topic.Contains(_adafruitSetting.HumidityTopicPath))
            OnHumidityMessageReceived(topic, decodedMessage);
        else if (args.ApplicationMessage.Topic.Contains(_adafruitSetting.TemperatureTopicPath))
            OnTemperatureMessageReceived(topic, decodedMessage);
        else

        Console.WriteLine("Message received: " + decodedMessage);
        return Task.CompletedTask;
    }
    
    
    private void OnHumidityMessageReceived(string topic, string content)
    {
        AccumulateNewValues(content);
    }
    
    private void OnTemperatureMessageReceived(string topic, string content)
    {
        AccumulateNewValues(content);
    }
    
    private void AccumulateNewValues(string content)
    {
        var values = content[2..].Split(';').Select(float.Parse).ToList();
        
        foreach (var value in values)
        {
            Console.WriteLine("Accumulating value: "+ + value);
        }
        
    }
    
    
    private string DecodeMqttPayload(byte[] encodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(Convert.ToBase64String(encodedData));
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

}