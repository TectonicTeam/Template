using ClubIdentity.Core;
using BC.TempateFull.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BC.TempateFull.Api.Controllers.Drivers
{
    [ApiExplorerSettings(GroupName = "DriverApp")]
    [Authenticated]
    [Route("DriverApp/[controller]")]
    [ApiController]
    public class DriverController(IServiceProvider serviceProvider, ApplicationConfiguration applicationConfiguration, EndpointDataSource endpointDataSource) : ControllerBase
    {

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }





    }
}
