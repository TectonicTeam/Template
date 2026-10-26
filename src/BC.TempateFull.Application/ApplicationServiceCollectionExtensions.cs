using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BC.TempateFull.Application;

public static class ApplicationServiceCollectionExtensions
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddSingleton<ApplicationConfiguration>();

        return services;
    }




}