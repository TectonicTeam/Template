using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

namespace BC.TempateFull.Infra.Persistence;

public class WebHookDbContextFactory : IDesignTimeDbContextFactory<ServiceDbContext>
{
    public ServiceDbContext CreateDbContext(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        string? connectionString;
        if (args.Length == 0)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            connectionString = configuration.GetConnectionString(nameof(ServiceDbContext));
        }
        else
        {
            connectionString = args[0];
        }
        Console.WriteLine("ASPNETCORE_ENVIRONMENT: " + env);

        Console.WriteLine(connectionString);
        var builder = new DbContextOptionsBuilder<ServiceDbContext>();
        builder.UseSqlServer(
              connectionString,
              o => o.MigrationsHistoryTable(
                  tableName: HistoryRepository.DefaultTableName,
                  schema: BC.TempateFull.Infra.Constants.Schema
              )
          //.MigrationsAssembly("FormMigrations")
          );
        return new ServiceDbContext(builder.Options);
    }
}