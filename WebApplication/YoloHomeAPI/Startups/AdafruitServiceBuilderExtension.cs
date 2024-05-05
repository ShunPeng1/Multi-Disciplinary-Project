using MQTTnet;
using YoloHomeAPI.Controllers;
using YoloHomeAPI.Services;
using YoloHomeAPI.Services.Interfaces;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Startups;

public static class AdafruitServiceBuilderExtension
{
    public static void AddAdafruitServices(this WebApplicationBuilder builder)
    {
        
        var adafruitSettings = new AdafruitSettings();
        builder.Configuration.Bind("AdafruitSettings", adafruitSettings);
        builder.Services.AddSingleton(adafruitSettings);
        
        builder.Services.AddHostedService<AdafruitMqttService>(); 

        builder.Services.AddSingleton<MQTTnet.Client.IMqttClient>(sp =>
        {
            var factory = new MqttFactory();
            return factory.CreateMqttClient();
        });

        builder.Services.AddSingleton<IAdafruitMqttService, AdafruitMqttService>();

        builder.Services.AddSingleton<IIotDeviceService, IotDeviceService>();

        builder.Services.AddScoped<IActivityLogService, ActivityLogService>();

        builder.Services.AddScoped<IManualControlService, ManualControlService>();
        
        builder.Services.AddScoped<IUserService, UserService>();
        
    }
    
}