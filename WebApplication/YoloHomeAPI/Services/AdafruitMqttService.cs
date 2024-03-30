using System.Text;
using MQTTnet;
using MQTTnet.Client;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Services;



public class AdafruitMqttService : BackgroundService
{
    private const float SEND_DATA_BACK_TIMER = 120f;

    public event Action<string> AnnounceMessageArrived; 

    private readonly AdafruitSettings _adafruitSetting;
    
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private IMqttClient _mqttClient;

    private string _accumulatorStartTimeString = DateTime.UtcNow.ToString();

    
    public AdafruitMqttService(AdafruitSettings adafruitSetting, IServiceScopeFactory serviceScopeFactory)
    {
        _adafruitSetting = adafruitSetting;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async override Task StartAsync(CancellationToken cancellationToken)
    {
        await base.StartAsync(cancellationToken);

        var mqttFactory = new MqttFactory();
        _mqttClient = mqttFactory.CreateMqttClient();

        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("io.adafruit.com", 8883).WithCredentials(_adafruitSetting.AdafruitUsername, _adafruitSetting.AdafruitKey).WithTls().Build();
        _mqttClient.ApplicationMessageReceivedAsync += ApplicationMessageReceivedHandler;
        await _mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
            .WithTopicFilter(f => { f.WithTopic(_adafruitSetting.AnnounceTopicPath); })
            .WithTopicFilter(f => { f.WithTopic(_adafruitSetting.SensorTopicPath); })
            .Build();

        await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

        AnnounceMessageArrived += InitializeMessageReceivedHandler;
        
        Console.WriteLine("AdafruitMqttService initialized successfully.");
    }

    public async override Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        await _mqttClient.DisconnectAsync();
        
        AnnounceMessageArrived -= InitializeMessageReceivedHandler;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
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
            PublishMessage(_helperService.MoistureTopicPath, averagedMoistureValue.ToString("0.00"));

            foreach (var accumulatedPlantDataLog in _accumulatedPlantDataLogs)
            {
                var ownerPlant = dbContext.PlantInformations.FirstOrDefault(info => info.Id == accumulatedPlantDataLog.PlantId);
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
        var applicationMessage = new MqttApplicationMessageBuilder().WithTopic(topic).WithPayload(content).Build();
        await _mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
    }

    private Task ApplicationMessageReceivedHandler(MqttApplicationMessageReceivedEventArgs args)
    {
        var decodedMessage = DecodeMqttPayload(args.ApplicationMessage.Payload);
        Console.WriteLine("From topic: " + args.ApplicationMessage.Topic);

        if (args.ApplicationMessage.Topic.Contains(_adafruitSetting.AdafruitAnnounceFeedName))
            OnAnnounceMessageReceived(decodedMessage);
        else if (args.ApplicationMessage.Topic.Contains(_adafruitSetting.AdafruitSensorFeedName))
            OnSensorMessageReceived(decodedMessage);

        Console.WriteLine("Message received: " + decodedMessage);
        return Task.CompletedTask;
    }

    private void OnAnnounceMessageReceived(string content)
    {
        AnnounceMessageArrived?.Invoke(content);
    }

    private void InitializeMessageReceivedHandler(string content)
    {
        if (content == "0;I")
        {
            using var scope = _serviceScopeFactory.CreateScope();
            //var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
            //int plantCount = dbContext.PlantInformations.Count();
            //PublishMessage(_adafruitSetting.AnnounceTopicPath, plantCount + ";ID");
        }
    }

    
    private void OnSensorMessageReceived(string content)
    {
        //AccumulateNewValues(content);
    }
    

    /*
    private void AccumulateNewValues(string content)
    {
        var values = content[2..].Split(';').Select(float.Parse).ToList();

        if (values.Count > _accumulatedPlantDataLogs.Count)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();

                for (int i = _accumulatedPlantDataLogs.Count; i < values.Count; i++)
                {
                    _accumulatedPlantDataLogs.Add(new AccumulatedPlantDataLog()
                    {
                        PlantId = dbContext.PlantInformations.ToList()[i].Id,
                    });
                }
            }
        }

        for (var i = 0; i < values.Count; i++)
        {
            switch (content[0])
            {
                case 'L':
                    _accumulatedPlantDataLogs[i].AccumulatedLightValue += values[i];
                    _accumulatedPlantDataLogs[i].LightValueCount++;
                    break;
                case 'T':
                    _accumulatedPlantDataLogs[i].AccumulatedTemperatureValue += values[i];
                    _accumulatedPlantDataLogs[i].TemperatureValueCount++;
                    break;
                case 'M':
                    _accumulatedPlantDataLogs[i].AccumulatedMoistureValue += values[i];
                    _accumulatedPlantDataLogs[i].MoistureValueCount++;
                    break;
            }
        }
        
    }
    
    */
    
    private string DecodeMqttPayload(byte[] encodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(Convert.ToBase64String(encodedData));
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }
}