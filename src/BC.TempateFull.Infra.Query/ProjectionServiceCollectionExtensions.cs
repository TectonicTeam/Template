using Catalog.Infra.Queries.Repositories;
using BC.TempateFull.Application.Query.Todos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BC.TempateFull.Infra.Query;

public static class ProjectionServiceCollectionExtensions
{
    public static IServiceCollection AddQuery(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDbContext<QueryDbContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseSqlServer(configuration.GetConnectionString("ServiceDbContext"));
            
            if (!environment.IsProduction())
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
                options.LogTo(Console.WriteLine);
            }
        });


        services.AddScoped<ITodoQueryRepository, TodoQueryRepository>();

        return services;
    }
}