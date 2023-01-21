using System.Text.Json.Serialization;
using BookAFlight.Entities;
using BookAFlight.Context;
using BookAFlight.JWT;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookAFlight.Services;

namespace BookAFlight;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var authenticationSettings = new AuthSettings();

        builder.Services.AddControllers();
        builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
        builder.Services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<devEnvDbContext>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IFleetService, FleetService>();
        builder.Services.AddScoped<IFlightService, FlightService>();

        builder.Services.AddSingleton(authenticationSettings);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(config =>
        {
            config.RequireHttpsMetadata = false;
            config.SaveToken = true;
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = authenticationSettings.JwtIssuer,
                ValidAudience = authenticationSettings.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
            };
        });

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

