using Dekacore.Domain;
using Dekacore.Persistence;
using Dekacore.Persistence.EntityFramework;
using Dekacore.Persistence.EntityFramework.Auditing;
using Dekacore.Persistence.EntityFramework.Concurrency;
using Dekacore.Persistence.EntityFramework.EventPublisher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BC.TempateFull.Infra.Persistence;

public static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(services);

        

        services.AddApplicationDbContext<ServiceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ServiceDbContext"));
            options.AddEventPublisherInterceptor();
            options.AddEntityAuditingInterceptor();
            options.AddConcurrencyTokenInterceptor();
            options.EnableSensitiveDataLogging();
            if (!environment.IsProduction())
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
                options.LogTo(Console.WriteLine);
            }
        });

        return services;
    }

   
}