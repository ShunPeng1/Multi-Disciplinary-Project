
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using YoloHomeAPI.Services;
using YoloHomeAPI.Services.Interfaces;
using YoloHomeAPI.Settings;

namespace YoloHomeAPI.Startups;

public static class AuthenticationServiceBuilderExtension
{
    
    public static void AddAuthenticationServices(this WebApplicationBuilder builder)
    {
        var settings = new AuthenticationSettings();
        builder.Configuration.Bind("AuthenticationSettings", settings);
        builder.Services.AddSingleton(settings);
        
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        
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

    }
    
}