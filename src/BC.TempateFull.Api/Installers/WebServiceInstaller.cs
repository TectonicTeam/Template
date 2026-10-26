using ClubIdentity.Core;
using Dekacore.Web;
using Dekacore.Web.Handlers;
using Dekacore.Web.Installer;
using Dekacore.Web.Serialization;
using BC.TempateFull.Api.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;


namespace BC.TempateFull.Api.Installers;

internal sealed class WebServiceInstaller : IServiceInstaller
{
    public void Install(IHostApplicationBuilder builder)
    {
        // Get the executing assembly
        var assembly = Assembly.GetExecutingAssembly();

        // Get the version attribute
        //var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
        //              ?? assembly.GetName().Version.ToString();

        var version = assembly!.GetName()!.Version!.ToString();

        var configuration = builder.Configuration;

        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            //some options
            options.SetDefaultCulture("fa");
        });
        builder.Services.AddLocalization();


        builder.Services.AddExceptionHandler<GlobalBusinessExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ClientApiActionFilter>();
        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new NullableStringToLongConverter());
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.Converters.Add(new InvariantDateTimeConverter());
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("AdminPanel", new OpenApiInfo
            {
                Title = $"{configuration["ServiceName"]} - AdminPanel {builder.Environment.EnvironmentName}",
                Description = $"AdminPanel API | v{version}"

            });
            options.SwaggerDoc("PassengerApp", new OpenApiInfo
            {
                Title = $"{configuration["ServiceName"]} - PassengerApp {builder.Environment.EnvironmentName}",
                Description = $"PassengerApp API | v{version}"

            });
            options.SwaggerDoc("RequesterPanel", new OpenApiInfo
            {
                Title = $"{configuration["ServiceName"]} - RequesterPanel {builder.Environment.EnvironmentName}",
                Description = $"RequesterPanel API | v{version}"

            });
            options.SwaggerDoc("ContractorPanel", new OpenApiInfo
            {
                Title = $"{configuration["ServiceName"]} - ContractorPanel {builder.Environment.EnvironmentName}",
                Description = $"ContractorPanel API | v{version}"

            });
            options.SwaggerDoc("DriverApp", new OpenApiInfo
            {
                Title = $"{configuration["ServiceName"]} - DriverApp {builder.Environment.EnvironmentName}",
                Description = $"DriverApp API | v{version}"

            });

            options.SwaggerDoc("ClientApi", new OpenApiInfo
            {
                Title = $"{configuration["ServiceName"]} - Client APIs - {builder.Environment.EnvironmentName}",
                Description = $"Client API | v{version}"
            });
            // Use the [ApiExplorerSettings] attributes to control grouping
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                return string.Equals(apiDesc.GroupName, docName, StringComparison.OrdinalIgnoreCase);
            });


            // To Enable authorization using Swagger (JWT)
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter ‘Bearer’ [space] and then your valid token in the text input below.\r\n\r\nExample: \'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\'",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });


        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Default", policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });

        builder.Services.AddScoped<AuthenticatedAttribute>();
        builder.Services.AddWorkingContext();
    }
}