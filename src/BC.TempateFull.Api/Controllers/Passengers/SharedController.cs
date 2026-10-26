using ClubIdentity.Core;
using BC.TempateFull.Api.Middlewares;
using BC.TempateFull.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Text.Json;

namespace BC.TempateFull.Api.Controllers.Passengers
{
    [ApiExplorerSettings(GroupName = "PassengerApp")]
    [Authenticated]
    [Route("PassengerApp/[controller]")]
    [ApiController]
    public class SharedController(IServiceProvider serviceProvider, ApplicationConfiguration applicationConfiguration, EndpointDataSource endpointDataSource) : ControllerBase
    {




        [HttpGet("endpoints")]
        public string GetAllEndpoints()
        {
            var endpoints = endpointDataSource.Endpoints
                .Where(e => e.Metadata.GetMetadata<ApiExplorerSettingsAttribute>()?.GroupName == "PassengerApp")
                .OfType<RouteEndpoint>()
                .Select(e =>
                {
                    var action = e.Metadata.GetMetadata<ControllerActionDescriptor>();
                    var method = action?.MethodInfo;
                    var parameters = e.Metadata.GetMetadata<ParametersAttribute>()?.Parameters
                                     ?? method?.GetParameters()
                                         .FirstOrDefault(x => x.Name == "request")
                                         ?.ParameterType.FullName
                                     ?? string.Empty;

                    return new
                    {
                        Controller = action?.ControllerName,
                        RouteName = e.RoutePattern.RawText?.Split('/').LastOrDefault() ?? string.Empty,
                        Route = e.RoutePattern.RawText,
                        e.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault()?.HttpMethods,
                        Action = action?.ActionName,
                        Parameters = parameters
                    };
                })
                .GroupBy(e => e.Controller)
                .ToDictionary(
                    g => g.Key!,
                    g => g.ToDictionary(e => e.RouteName, e => e)
                );

            return JsonSerializer.Serialize(endpoints, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
}
