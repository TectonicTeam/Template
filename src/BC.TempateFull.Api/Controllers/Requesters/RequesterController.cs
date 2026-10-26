using ClubIdentity.Core;
using BC.TempateFull.Api.Middlewares;
using BC.TempateFull.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using System.Text.Json;

namespace BC.TempateFull.Api.Controllers.Requesters
{
    [ApiExplorerSettings(GroupName = "RequesterPanel")]
    [Authenticated]
    [Route("RequesterPanel/[controller]")]
    [ApiController]
    public class RequesterController(IServiceProvider serviceProvider, ApplicationConfiguration applicationConfiguration, EndpointDataSource endpointDataSource) : ControllerBase
    {

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }





    }
}
