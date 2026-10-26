using Dekacore.Observability.Logging.Serilog;
using Dekacore.Observability.Logging.Serilog.Options;
using BC.TempateFull.Application;
using Microsoft.Identity.Client;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace BC.TempateFull.Api.Installers
{
    public static class SerilogServiceExtension
    {
        public static void UseServiceSerilog(this WebApplicationBuilder builder)
        {

            

            var elasticAddress = Environment.GetEnvironmentVariable("ELASTIC_ADDRESS");

            SerilogApplicationEnricherOptions enrciherOption = new SerilogApplicationEnricherOptions()
            {
                ApplicationName = "",
                ServiceName = builder.Configuration.GetValue<string>("ServiceName"),
                ServiceVersion = Assembly.GetExecutingAssembly().GetName().Version!.ToString()
            };


            if (!string.IsNullOrEmpty(elasticAddress))
            {

                var elasticUSER = Environment.GetEnvironmentVariable("ELASTIC_USER");
                var elasticPASS = Environment.GetEnvironmentVariable("ELASTIC_PASS");

              

                builder.UseSerilog(
                    enricherOption: enricher =>
                    {
                        enricher.ApplicationName = enrciherOption.ApplicationName;
                        enricher.ServiceName = enrciherOption.ServiceName;
                        enricher.ServiceVersion = enrciherOption.ServiceVersion;

                    },
                loggerConfigAction: logger =>
                {
                    logger.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticAddress!))
                    {
                        IndexFormat = $"p6m-deka-webHook-{builder.Environment.EnvironmentName.ToLower()}-{string.Format("{0:yyyy.MM}", DateTime.Now)}",
                        AutoRegisterTemplate = true,
                        ModifyConnectionSettings = x =>
                        {
                            x.BasicAuthentication(elasticUSER, elasticPASS);
                            x.DisableAutomaticProxyDetection();
                            x.ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => { return true; });
                            return x;
                        }

                    });

                    if (builder.Environment.IsDevelopment())
                    {
                        logger.WriteTo.Console();
                    }
                });


            }
            else if (builder.Configuration.GetSection("Serilog").Exists())
            {
                builder.UseSerilog("Serilog");
            }
            else
            {
                builder.UseSerilog(
                    enricherOption: enricher =>
                {
                    enricher.ApplicationName = enrciherOption.ApplicationName;
                    enricher.ServiceName = enrciherOption.ServiceName;
                    enricher.ServiceVersion = enrciherOption.ServiceVersion;

                },
                    loggerConfigAction: logger =>
                    {
                        logger.WriteTo.Console();
                    }

                );
            }

            builder.Logging.AddConsole(Console.WriteLine);

        }
    }
}
