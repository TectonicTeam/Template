using Dekacore.Toolkit.BackgroundTask.Job;
using Dekacore.Toolkit.ScheduleTask.HangfireScheduleTask;
using Dekacore.Web.Installer;
using BC.TempateFull.Application;
using BC.TempateFull.Application.Command;
using BC.TempateFull.Application.Query;
using BC.TempateFull.Infra.Persistence;
using BC.TempateFull.Infra.Query;
using Hangfire;
using Hangfire.SqlServer;

namespace BC.TempateFull.Api.Installers;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IHostApplicationBuilder builder)
    {

        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        builder.Configuration
          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
          .AddJsonFile("appsettings.json", false, false)
          .AddJsonFile($"appsettings.{env}.json", true, false)
          .Build();

        builder.Services.AddApplicationServices(builder.Configuration, builder.Environment);
        builder.Services.AddApplicationCommandServices(builder.Configuration, builder.Environment);
        builder.Services.AddApplicationQueryServices(builder.Configuration, builder.Environment);
        builder.Services.AddPersistence(builder.Configuration, builder.Environment);
        builder.Services.AddQuery(builder.Configuration, builder.Environment);
        //builder.Services.AddExternalServices(builder.Configuration, builder.Environment);
        #region Schedule Task
        var hangfireConfig = builder.Configuration.GetSection("HangfireConfig").Get<HangfireConfig>();
        //builder.Services.AddHangfire(hangfireConfig!);

        builder.Services.AddHangfire(configuration => configuration
          .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(hangfireConfig.ConnectionString, new SqlServerStorageOptions
          {
              CommandBatchMaxTimeout = TimeSpan.FromMinutes(hangfireConfig.CommandBatchMaxTimeout),
              SlidingInvisibilityTimeout = TimeSpan.FromMinutes(hangfireConfig.SlidingInvisibilityTimeout),
              QueuePollInterval = hangfireConfig.QueuePollInterval,
              UseRecommendedIsolationLevel = hangfireConfig.UseRecommendedIsolationLevel,
              DisableGlobalLocks = hangfireConfig.DisableGlobalLocks
          }));

        // Add the processing server as IHostedService
        builder.Services.AddHangfireServer();
        builder.Services.AddBackgroundJobs<ServiceDbContext>();
        #endregion

    }
}
