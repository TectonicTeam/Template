using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MsSql;

namespace DekaClub.IntegrationTest.Base;

// ReSharper disable once ClassNeverInstantiated.Global
public class WebApplicationTestFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Console.Write("Container connection string: {0}", _msSqlContainer.GetConnectionString());
        builder.ConfigureServices((context, services) =>
        {
            context.Configuration["ConnectionStrings:CatalogDbContext"] = _msSqlContainer.GetConnectionString();
        });
        builder.UseEnvironment("Test");
    }

    public Task InitializeAsync()
    {
        return _msSqlContainer.StartAsync();
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return _msSqlContainer.StopAsync();
    }
}