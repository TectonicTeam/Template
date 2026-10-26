using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Dekacore.Observability.Metric.OpenTelemetry.Extensions;
using Dekacore.Web.Installer;

namespace BC.TempateFull.Api.Installers;

public class ObservabilityServiceInstaller : IServiceInstaller
{
    public void Install(IHostApplicationBuilder builder)
    {
        builder.Services.AddOpenTelemetry()
            .WithDekacoreMetrics(providerBuilder => { providerBuilder.AddConsoleExporter(); })
            .WithTracing(providerBuilder => { providerBuilder.AddConsoleExporter(); });
    }
}