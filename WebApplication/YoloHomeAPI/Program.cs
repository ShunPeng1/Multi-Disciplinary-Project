using System.Text;
using System.Diagnostics;

using Microsoft.IdentityModel.Tokens;

using YoloHomeAPI;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using MQTTnet;
using YoloHomeAPI.Controllers;
using YoloHomeAPI.Services;
using YoloHomeAPI.Services.Interfaces;
using YoloHomeAPI.Settings;
using YoloHomeAPI.Startups;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var settings = new AuthenticationSettings();
builder.Configuration.Bind("AuthenticationSettings", settings);
builder.Services.AddSingleton(settings);

var adafruitSettings = new AdafruitSettings();
builder.Configuration.Bind("AdafruitSettings", adafruitSettings);
builder.Services.AddSingleton(adafruitSettings);

builder.Services.AddScoped<IAuthenticationService, MockAuthenticationService>();
builder.Services.AddScoped<IAdafruitMqttService, AdafruitMqttService>();

builder.Services.AddHostedService<AdafruitMqttService>(); 

builder.Services.AddSingleton<MQTTnet.Client.IMqttClient>(sp =>
{
    var factory = new MqttFactory();
    return factory.CreateMqttClient();
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
        {
            NamingStrategy = new Newtonsoft.Json.Serialization.DefaultNamingStrategy() // Use Camel Case
        };
    }
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtKey))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.BuildReactApp();
app.ServeReactApp();

app.Run();