using ClubIdentity.Core;
using BC.TempateFull.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BC.TempateFull.Api.Controllers.Contractors
{
    [ApiExplorerSettings(GroupName = "ContractorPanel")]
    [Authenticated]
    [Route("ContractorPanel/[controller]")]
    [ApiController]
    public class ContractorController(IServiceProvider serviceProvider, ApplicationConfiguration applicationConfiguration, EndpointDataSource endpointDataSource) : ControllerBase
    {

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }





    }
}
