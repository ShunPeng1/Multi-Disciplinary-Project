using System.Globalization;
using YoloHomeAPI.Controllers;
using YoloHomeAPI.Data;
using YoloHomeAPI.Repositories.Interfaces;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Services.Interfaces;

public class IotDeviceService : IIotDeviceService
{
    private readonly AdafruitSettings _adafruitSettings;
    
    private readonly IIotDeviceRepo _iotDeviceRepo;
    private readonly ISensorDataRepo _sensorDataRepo;
    private readonly IOwnerRepo _ownerRepo;
    
    private List<IotDeviceData> _devices = new();
    
    public IotDeviceService(AdafruitSettings adafruitSettings , IIotDeviceRepo iotDeviceRepo, ISensorDataRepo sensorDataRepo, IOwnerRepo ownerRepo)
    {
        _adafruitSettings = adafruitSettings;
        
        _iotDeviceRepo = iotDeviceRepo;
        _sensorDataRepo = sensorDataRepo;
        _ownerRepo = ownerRepo;

        MockDeviceCreation();
        
        
    }
    

    private async void MockDeviceCreation()
    {
        var devices = (await _iotDeviceRepo.GetAllAsync()).ToList();
        if (devices.Count != 0)
        {
            _devices = devices;
            return;
        }
        
        var doorSensor = new IotDeviceData()
        {
            DeviceId = Guid.NewGuid(),
            DeviceName = "Door Sensor",
            DeviceType = "Door",
            DeviceLocation = "Living Room",
            DeviceState = "Running"
        };
        await _iotDeviceRepo.AddAsync(doorSensor);
        _devices.Add(doorSensor);
        
        var temperatureSensor = new IotDeviceData()
        {
            DeviceId = Guid.NewGuid(),
            DeviceName = "Temperature Sensor",
            DeviceType = "Temper",
            DeviceLocation = "Living Room",
            DeviceState = "Running"
        };
        await _iotDeviceRepo.AddAsync(temperatureSensor);
        _devices.Add(temperatureSensor);
        
        var humiditySensor = new IotDeviceData()
        {
            DeviceId = Guid.NewGuid(),
            DeviceName = "Humidity Sensor",
            DeviceType = "Humidity",
            DeviceLocation = "Kitchen",
            DeviceState = "Running"
        };
        await _iotDeviceRepo.AddAsync(humiditySensor);
        _devices.Add(humiditySensor);
        
        var lightSensor = new IotDeviceData()
        {
            DeviceId = Guid.NewGuid(),
            DeviceName = "Light Sensor",
            DeviceType = "Light",
            DeviceLocation = "Living Room",
            DeviceState = "Running"
        };
        await _iotDeviceRepo.AddAsync(lightSensor);
        
        var light2Sensor = new IotDeviceData()
        {
            DeviceId = Guid.NewGuid(),
            DeviceName = "Light Sensor",
            DeviceType = "Light2",
            DeviceLocation = "Bath Room",
            DeviceState = "Running"
        };
        await _iotDeviceRepo.AddAsync(lightSensor);
        
        var light3Sensor = new IotDeviceData()
        {
            DeviceId = Guid.NewGuid(),
            DeviceName = "Light Sensor",
            DeviceType = "Light3",
            DeviceLocation = "Kitchen Room",
            DeviceState = "Running"
        };
        await _iotDeviceRepo.AddAsync(light3Sensor);

        var light4Sensor = new IotDeviceData()
        {
            DeviceId = Guid.NewGuid(),
            DeviceName = "Light Sensor",
            DeviceType = "Light4",
            DeviceLocation = "Bed Room",
            DeviceState = "Running"
        };
        await _iotDeviceRepo.AddAsync(light4Sensor);
        
        var fanSensor = new IotDeviceData()
        {
            DeviceId = Guid.NewGuid(),
            DeviceName = "Fan Sensor",
            DeviceType = "Fan",
            DeviceLocation = "Bed Room",
            DeviceState = "Running"
        };
        await _iotDeviceRepo.AddAsync(fanSensor);
        
    }

    public async Task<IIotDeviceService.IotDeviceResult> AddOwner(string username)
    {
        foreach (var device in _devices)
        {
            await _ownerRepo.AddAsync(new OwnerData()
            {
                UserName = username,
                DeviceId = device.DeviceId,
                Recurrence = "Daily",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now + TimeSpan.FromDays(365)
                
            });
        }
        
        return new IIotDeviceService.IotDeviceResult(true, null!);
        
    }

    public async Task<IIotDeviceService.IotDeviceResult> GetAllDevices(string username)
    {
        var devices = (await _iotDeviceRepo.GetAllByUserNameAsync(username)).ToList();
        return new IIotDeviceService.IotDeviceResult(true, devices);
       
    }

    public async Task<IIotDeviceService.SensorDataResult> GetAllSensorData(string type, DateTime start, DateTime end)
    {
        var deviceId =  _devices.Find(device => device.DeviceType == type)?.DeviceId ?? throw new Exception("Device not found");
        
        var sensorData= await _sensorDataRepo.GetAllAsync(deviceId, DateTime.MinValue, DateTime.Now);
        
        var listData = (sensorData.OrderByDescending(data => data.TimeStamp)).ToList(); 
        
        return new IIotDeviceService.SensorDataResult(true, listData);
    }

    public async Task<IIotDeviceService.SensorDataResult> GetLatestSensorData(string type)
    {
        var deviceId =  _devices.Find(device => device.DeviceType == type)?.DeviceId ?? throw new Exception("Device not found");
        
        var sensorData= await _sensorDataRepo.GetAllAsync(deviceId, DateTime.MinValue, DateTime.Now);
        
        var listData = (sensorData.OrderByDescending(data => data.TimeStamp)).ToList(); 
        var latestData = listData.Count > 0 ? listData[0] : null!;
        
        return new IIotDeviceService.SensorDataResult(true, new List<SensorData>(){latestData});
        
    }

    public Task<IIotDeviceService.IotDeviceResult> AddSensorDataAsync(AdafruitDataReceiveData data)
    {
        var topic = data.Topic;
        if (topic.Contains(_adafruitSettings.HumidityTopicPath))   
            HumidityMessageReceivedHandler(data);
        else if (topic.Contains(_adafruitSettings.TemperatureTopicPath))
            TemperatureMessageReceivedHandler(data);
        else if (topic.Contains(_adafruitSettings.LightTopicPath))
            LightMessageReceivedHandler(data, "Light");
        else if (topic.Contains(_adafruitSettings.Light2TopicPath))
            LightMessageReceivedHandler(data, "Light2");
        else if (topic.Contains(_adafruitSettings.Light3TopicPath))
            LightMessageReceivedHandler(data, "Light3");
        else if (topic.Contains(_adafruitSettings.Light4TopicPath))
            LightMessageReceivedHandler(data, "Light4");
        else if (topic.Contains(_adafruitSettings.FanTopicPath))
            FanReceivedHandler(data);
        else if (topic.Contains(_adafruitSettings.DoorTopicPath))
            DoorMessageReceivedHandler(data);
        else
        {
            Console.WriteLine("Invalid topic");
            return Task.FromResult(new IIotDeviceService.IotDeviceResult(false, null!));
        }
        
        return Task.FromResult(new IIotDeviceService.IotDeviceResult(true, null!));
    }

    
    private void LightMessageReceivedHandler(AdafruitDataReceiveData obj, string type)
    {
        IotDeviceData deviceId = GetLightDevice(type);
        var values = obj.Values;

        foreach (var value in values)
        {
            var sensorData = new SensorData()
            {
                DeviceId = deviceId.DeviceId,
                TimeStamp = DateTime.Now,
                Value = value.ToString(CultureInfo.CurrentCulture)
            };
            _sensorDataRepo.AddAsync(sensorData);
        }
    }
    
    private IotDeviceData GetLightDevice(string type)
    {
        return _devices.Find(device => device.DeviceType == type) ?? throw new Exception("Device not found");
    }

    private void FanReceivedHandler(AdafruitDataReceiveData obj)
    {
        IotDeviceData deviceId = _devices.Find(device => device.DeviceType == "Fan") ?? throw new Exception("Device not found");
        var values = obj.Values;

        foreach (var value in values)
        {
            var sensorData = new SensorData()
            {
                DeviceId = deviceId.DeviceId,
                TimeStamp = DateTime.Now,
                Value = value.ToString(CultureInfo.CurrentCulture)
            };
            _sensorDataRepo.AddAsync(sensorData);
        }
    }

    private void DoorMessageReceivedHandler(AdafruitDataReceiveData obj)
    {
        IotDeviceData deviceId = _devices.Find(device => device.DeviceType == "Door") ?? throw new Exception("Device not found");
        var values = obj.Values;

        foreach (var value in values)
        {
            var sensorData = new SensorData()
            {
                DeviceId = deviceId.DeviceId,
                TimeStamp = DateTime.Now,
                Value = value.ToString(CultureInfo.CurrentCulture)
            };
            _sensorDataRepo.AddAsync(sensorData);
        }
    }

    private void TemperatureMessageReceivedHandler(AdafruitDataReceiveData obj)
    {
        IotDeviceData deviceId = _devices.Find(device => device.DeviceType == "Temper") ?? throw new Exception("Device not found");
        var values = obj.Values;

        foreach (var value in values)
        {
            var sensorData = new SensorData()
            {
                DeviceId = deviceId.DeviceId,
                TimeStamp = DateTime.Now,
                Value = value.ToString(CultureInfo.CurrentCulture)
            };
            _sensorDataRepo.AddAsync(sensorData);
        }
    }

    private void HumidityMessageReceivedHandler(AdafruitDataReceiveData obj)
    {
        IotDeviceData deviceId = _devices.Find(device => device.DeviceType == "Humidity") ?? throw new Exception("Device not found");
        var values = obj.Values;

        foreach (var value in values)
        {
            var sensorData = new SensorData()
            {
                DeviceId = deviceId.DeviceId,
                TimeStamp = DateTime.Now,
                Value = value.ToString(CultureInfo.CurrentCulture)
            };
            _sensorDataRepo.AddAsync(sensorData);
        }
    }
    
}