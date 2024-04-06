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

        builder.Services.AddScoped<IIotDeviceRepo, IotDeviceRepo>();
        builder.Services.AddScoped<ISensorDataRepo, SensorDataRepo>();
        builder.Services.AddScoped<IOwnerRepo, OwnerRepo>();
        builder.Services.AddScoped<IUserRepo, UserRepo>();
        builder.Services.AddScoped<IActivityLogRepo, ActivityLogRepo>();

    }
    
}