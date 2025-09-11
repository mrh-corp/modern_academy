using System.Reflection;
using System.Text;
using System.Text.Json;
using Amazon.S3;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Service;
using Infrastructure.Authentication;
using Infrastructure.Authorization;
using Infrastructure.Database;
using Infrastructure.DomainEvents;
using Infrastructure.Time;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SharedKernel;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddServices()
            .AddDatabase(configuration)
            .AddHealthChecks(configuration)
            .AddAuthenticationInternal(configuration)
            .AddAuthorizationInternal()
            .AddS3Service(configuration);

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();
        
        Assembly[] assemblies = 
        [
            Assembly.Load("Infrastructure"), // services implementation
            Assembly.Load("Application")     // interfaces
        ];

        var serviceTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(type => type is { IsClass: true, IsAbstract: false } 
                           && typeof(IService).IsAssignableFrom(type))
            .ToList();

        foreach (Type serviceType in serviceTypes)
        {
            var interfaces = serviceType.GetInterfaces()
                .Where(i => i != typeof(IService) && typeof(IService).IsAssignableFrom(i))
                .ToList();

            foreach (Type @interface in interfaces)
            {
                services.AddScoped(@interface, serviceType);
            }
        }

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!);

        return services;
    }

    private static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };

                o.Events = new JwtBearerEvents()
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var error = Result.Failure(
                            Error.Problem("Token.NotProvided", "You are not authorized, Token missing or invalid"));
                        string json = JsonSerializer.Serialize(error);
                        return context.Response.WriteAsync(json);
                    }
                };
            });

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();

        return services;
    }

    private static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddScoped<PermissionProvider>();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }
    
    public static IServiceCollection AddS3Service(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection s3Config = configuration.GetSection("S3");
        services.AddSingleton<IAmazonS3>(sp => new AmazonS3Client(
            s3Config["AccessKey"],
            s3Config["SecretKey"],
            new AmazonS3Config
            {
                ServiceURL = s3Config["ServiceURL"],
                ForcePathStyle = true,
                AuthenticationRegion = s3Config["Region"],
            }
        ));
        return services;
    }
}
