using YoloHomeAPI.Repositories;
using YoloHomeAPI.Repositories.Interfaces;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Startups;

public static class PostgreSQLServiceBuilderExtension
{
    public static void AddPostgreSqlServices(this WebApplicationBuilder builder)
    {
        var settings = new DatabaseSettings();
        builder.Configuration.Bind("DatabaseSettings", settings);
        builder.Services.AddSingleton(settings);

        builder.Services.AddSingleton<IIotDeviceRepo, IotDeviceRepo>();
        builder.Services.AddSingleton<ISensorDataRepo, SensorDataRepo>();
        builder.Services.AddSingleton<IOwnerRepo, OwnerRepo>();
        builder.Services.AddSingleton<IUserRepo, UserRepo>();
        builder.Services.AddSingleton<IActivityLogRepo, ActivityLogRepo>();

    }
    
}