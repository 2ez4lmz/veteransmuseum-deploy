using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VeteransMuseum.Application.Abstractions.Authentication;
using VeteransMuseum.Application.Abstractions.Clock;
using VeteransMuseum.Application.Abstractions.Data;
using VeteransMuseum.Domain.Abstractions;
using VeteransMuseum.Domain.News;
using VeteransMuseum.Domain.Users;
using VeteransMuseum.Domain.Veterans;
using VeteransMuseum.Infrastructure.Authentication;
using VeteransMuseum.Infrastructure.Authorization;
using VeteransMuseum.Infrastructure.Clock;
using VeteransMuseum.Infrastructure.Data;
using VeteransMuseum.Infrastructure.Repositories;
using AuthenticationOptions = VeteransMuseum.Infrastructure.Authentication.AuthenticationOptions;
using AuthenticationService = VeteransMuseum.Infrastructure.Authentication.AuthenticationService;
using IAuthenticationService = VeteransMuseum.Application.Abstractions.Authentication.IAuthenticationService;

namespace VeteransMuseum.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        
        AddPersistence(services, configuration);
 
        AddAuthentication(services, configuration);
        
        AddAuthorization(services);

        return services;
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        
        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));
 
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        
        services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));
 
        services.AddTransient<AdminAuthorizationDelegatingHandler>();
         
        services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                KeycloakOptions keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;
 
                httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
            })
            .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();
        
        services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpClient) =>
            {
                KeycloakOptions keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;
     
                httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
            });
        
        services.AddHttpContextAccessor();
 
        services.AddScoped<IUserContext, UserContext>();
    }
    
    private static void AddAuthorization(IServiceCollection services)
    {
        services.AddScoped<AuthorizationService>();
 
        services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("Database") ??
            throw new ArgumentNullException(nameof(configuration));
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddScoped<IVeteranRepository, VeteranRepository>();
        
        services.AddScoped<INewsRepository, NewsRepository>();
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        
        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));
    }
}