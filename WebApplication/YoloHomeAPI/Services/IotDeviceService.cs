using System.Globalization;
using YoloHomeAPI.Controllers;
using YoloHomeAPI.Data;
using YoloHomeAPI.Repositories.Interfaces;

namespace YoloHomeAPI.Services.Interfaces;

public class IotDeviceService : IIotDeviceService
{
    private readonly IAdafruitMqttService _adafruitMqttService;
    private readonly IIotDeviceRepo _iotDeviceRepo;
    private readonly ISensorDataRepo _sensorDataRepo;
    private readonly IOwnerRepo _ownerRepo;
    
    private List<IotDeviceData> _devices = new();
    
    public IotDeviceService(IAdafruitMqttService adafruitMqttService, IIotDeviceRepo iotDeviceRepo, ISensorDataRepo sensorDataRepo, IOwnerRepo ownerRepo)
    {
        _adafruitMqttService = adafruitMqttService;
        _iotDeviceRepo = iotDeviceRepo;
        _sensorDataRepo = sensorDataRepo;
        _ownerRepo = ownerRepo;

        MockDeviceCreation();

        _adafruitMqttService.OnHumidityMessageReceived += HumidityMessageReceivedHandler;
        _adafruitMqttService.OnTemperatureMessageReceived += TemperatureMessageReceivedHandler;
        _adafruitMqttService.OnDoorMessageReceived += DoorMessageReceivedHandler;
        _adafruitMqttService.OnFanReceived += FanReceivedHandler;
        _adafruitMqttService.OnLightMessageReceived += LightMessageReceivedHandler;
        
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

    public async Task<IIotDeviceService.SensorDataResult> GetAllSensorData(Guid deviceId, DateTime start, DateTime end)
    {
        var sensorData = await _sensorDataRepo.GetAllAsync(deviceId, start, end);
        var stringData = sensorData.Select(data => data.Value).ToList();
        return new IIotDeviceService.SensorDataResult(true, stringData);
    }

    private void LightMessageReceivedHandler(AdafruitDataReceiveData obj)
    {
        IotDeviceData deviceId = _devices.Find(device => device.DeviceType == "Light") ?? throw new Exception("Device not found");
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