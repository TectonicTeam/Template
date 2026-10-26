using ClubIdentity.Core;
using Dekacore.Extension.Web;
using Dekacore.Observability.Logging.Serilog;
using Dekacore.Toolkit.BackgroundTask.Job;
using Dekacore.Toolkit.ScheduleTask.HangfireScheduleTask;
using Dekacore.Web;
using Dekacore.Web.Extension;
using BC.TempateFull.Api.Installers;
using BC.TempateFull.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.UseServiceSerilog();
builder.Logging.AddConsole(Console.WriteLine);
builder.InstallService<WebServiceInstaller>();
builder.InstallService<IdentityServiceInstaller>();
builder.InstallService<ApplicationServiceInstaller>();
builder.InstallService<MessagingServiceInstaller>();
builder.Services.AddImportExportExecutable();
//builder.InstallService<BC.TempateFullObservabilityServiceInstaller>();


var app = builder.Build();
if (app.Environment.IsProduction() || app.Environment.IsEnvironment("Staging"))
{
    var dbContext = app.Services.GetRequiredService<ServiceDbContext>();
    dbContext.Database.MigrateAsync().Wait();
}

app.UseServiceLocator();
app.UseCorrelation();
app.UseSerilogRequestLogging();
app.UseExceptionHandler();


//if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/AdminPanel/swagger.json", "Admin Panel");
        c.SwaggerEndpoint("/swagger/ClientApi/swagger.json", "Client APIs");
        c.SwaggerEndpoint("/swagger/PassengerApp/swagger.json", "Passengers App");
        c.SwaggerEndpoint("/swagger/RequesterPanel/swagger.json", "Requesters Panel");
        c.SwaggerEndpoint("/swagger/ContractorPanel/swagger.json", "Contractors Panel");
        c.SwaggerEndpoint("/swagger/DriverApp/swagger.json", "Drivers App");
    });
}

Console.WriteLine("Environment: " + app.Environment.EnvironmentName);

var assembly = Assembly.GetExecutingAssembly();
var version = assembly!.GetName()!.Version!.ToString();

Console.WriteLine("Version: " + version);


app.StartHangfireScheduleTasks();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseHttpsRedirection();
app.UseCors("Default");
app.UseAuthorization();
app.MapControllers();
app.SeedRequestPermissions();
app.UseEnricherFromRequest();
app.UseBackgroundJobEndpoint("AdminPanel", "AdminPanel");
app.Run();

namespace BC.TempateFull.Api
{
    public partial class Program;
}