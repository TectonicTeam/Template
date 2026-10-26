using ClubIdentity.Core.Permission.Middlewares;
using Dekacore.Domain;
using Dekacore.Extension.Mediator.Transaction;
using Dekacore.Persistence;
using Dekacore.Toolkit.ExcelImportAndExport.ExcelDataReader;
using Dekacore.Toolkit.IdGenerator.Snowflake;
using Dekacore.Toolkit.SMSProvider.Jiring;
using Dekacore.Toolkit.StorageProvider.File;
using Ds.FleetManagement.Domain.ToDo;
using Ds.FleetManagement.Domain.ToDo.Services;
using BC.TempateFull.Application.Command.DomainServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;


namespace BC.TempateFull.Application.Command;

public static class ApplicationCommandServiceCollectionExtensions
{
    public static Assembly Assembly => typeof(ApplicationCommandServiceCollectionExtensions).Assembly;

    public static IServiceCollection AddApplicationCommandServices(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSnowflake(x => { });


        #region StorageProvider

        var s3Endpoint = configuration["s3:endpoint"]!;
        var s3AccessKey = configuration["s3:accessKey"]!;
        var s3SecretKey = configuration["s3:secretKey"]!;
        //services.AddFileProvider(option => { option.BasePath = ""; });
        services.AddCeph(x => { x.S3AccessKey = s3AccessKey; x.S3Endpoint = s3Endpoint; x.S3SecretKey = s3SecretKey; });

        #endregion


        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(Assembly);
            x.UseTransactionalRequestPipeline();
            x.UseMemberRequestPipeline();
            x.UseAdminRequestPipeline();
        });


        services.AddScoped<ITodoNameUsageValidator, TodoNameUsageValidatorService>();



        services.AddScoped(GetRepositoryOf<ITodoRepository, Todo>);

        services.AddHttpClient();
        services.AddMemoryCache();


        services.AddExcelImportAndExport();

        return services;
    }


    private static TRepository GetRepositoryOf<TRepository, TEntity>(IServiceProvider serviceProvider)
    where TRepository : IDomainRepository<TEntity>
    where TEntity : Aggregate
    {
        return serviceProvider.GetRequiredService<IUnitOfWork>().GetRepository<TRepository, TEntity>();
    }


}