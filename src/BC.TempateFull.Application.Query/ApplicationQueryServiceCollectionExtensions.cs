using Dekacore.Extension.Mediator.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace BC.TempateFull.Application.Query;

public static class ApplicationQueryServiceCollectionExtensions
{
    private static Assembly Assembly => typeof(ApplicationQueryServiceCollectionExtensions).Assembly;

    public static IServiceCollection AddApplicationQueryServices(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(services);


        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(Assembly);
            x.UseCacheableRequestPipeline();
        });

        services.AddHttpClient();

        return services;
    }


}