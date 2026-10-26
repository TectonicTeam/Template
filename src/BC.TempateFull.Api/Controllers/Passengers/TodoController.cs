using ClubIdentity.Core;
using Dekacore.Abstraction.Query;
using Dekacore.Constant;
using Dekacore.Web.Controller;
using Dekacore.Web.Filters;
using BC.TempateFull.Application.Command.UseCases.Todos;
using BC.TempateFull.Application.Query.Todos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BC.TempateFull.Api.Controllers.Passengers;

[Authenticated]
[ApiExplorerSettings(GroupName = "PassengerApp")]
[Route("PassengerApp/[controller]")]
public class TodoController(IMediator mediator, ILogger<TodoController> logger)
    : ApiControllerBase(mediator)
{
    [HttpPost("CreateAnTodo")]
    public async Task<long> Post([FromBody] CreateAnTodoCommand request, CancellationToken token)
    {
        return await SendAsync(request, token);
    }



    [HttpPost("EditTodo")]
    [RequiredHeader(HeaderConstant.ConcurrencyToken)]
    public Task<long> Post([FromBody] EditTodoCommand request, CancellationToken token)
    {
        return SendAsync(request, token);
    }

    [HttpPost("DeleteTodo")]
    public Task<long> Post([FromBody] DeleteTodoCommand request, CancellationToken token)
    {
        return SendAsync(request, token);
    }

    [HttpGet("GetTodo")]
    public Task<GetTodoByIdResult> Get(long Id,
        CancellationToken token)
    {

        var request = new GetTodoByIdRequest(Id);
        return SendAsync(request, token);
    }

    [HttpPost("GetAllTodos")]
    public Task<IPagedList<GetAllTodosResult>> Post([FromBody] GetAllTodosRequest request,
        CancellationToken token)
    {
        return SendAsync(request, token);

    }

    [HttpPost("GetAllTodsExcel")]
    public Task<FileContentResult> Excel([FromBody] GetAllTodosRequest request,
        CancellationToken token)
    {
        return ExportToExcelAsync(request, token: token);

    }

    [HttpPost("GetAllTodosExcelBackground")]
    public Task<long> ExcelBackground([FromBody] GetAllTodosRequest request, string groupName,
        CancellationToken token)
    {

        return ExportToExcelBackgroundAsync(request, groupName, token: token);

    }



    [HttpPost("RequestToImporTodoExcel")]
    public Task<long> ExcelBackground([FromForm] ImportFromExcelForm request, string groupName, CancellationToken token)
    {
        return ImportFromExcelBackgroundAsync<CreateAnTodoCommand>(request, groupName, token: token);
    }



}