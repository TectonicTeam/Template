using Dekacore.Core.Exceptions;
using BC.TempateFull.Application;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BC.TempateFull.Api.Middlewares
{
    public class ClientApiActionFilter : IActionFilter
    {
        private readonly ILogger<ClientApiActionFilter> _logger;

        public ClientApiActionFilter(ILogger<ClientApiActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //var clientApiRequest = context.ActionArguments.Where(x => x.Value is IClientApiRequest).SingleOrDefault().Value as IClientApiRequest;
            //if (clientApiRequest is not null)
            //{
            //    var clientIdClaim = context.HttpContext.User.Claims.Where(x => x.Type == "ClientId").SingleOrDefault()?.Value;
            //    if (clientApiRequest.ClientId != int.Parse(clientIdClaim))
            //        BusinessException.Throw("AccessDenied!!");
            //}

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // You can perform post-action logic here if needed
        }
    }
}