using ClubIdentity.Core;
using BC.TempateFull.Api.Middlewares;
using BC.TempateFull.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using System.Text.Json;

namespace BC.TempateFull.Api.Controllers
{
    
    [Authenticated]
    [Route("[controller]")]
    [ApiController]
    public class SharedController(IServiceProvider serviceProvider, ApplicationConfiguration applicationConfiguration, EndpointDataSource endpointDataSource) : ControllerBase
    {


        [AllowAnonymous]
        [HttpGet("getInfo")]
        public IActionResult GetInfo()
        {
            // Get the executing assembly
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly!.GetName()!.Version!.ToString();


            return Ok(new { ServiceName = applicationConfiguration.GetServiceName(), Version = version, Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") });
        }



    }
}
