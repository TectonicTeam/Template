using Dekacore.Core.Exceptions;
using MediatR;

namespace BC.TempateFull.Application.Query.Todos;

public record GetTodoByIdRequest(long Id) : IRequest<GetTodoByIdResult>;

public class GetTodoByIdRequestHandler(ITodoQueryRepository repository)
    : IRequestHandler<GetTodoByIdRequest, GetTodoByIdResult>
{
    public async Task<GetTodoByIdResult> Handle(GetTodoByIdRequest request, CancellationToken token)
    {
        var result = await repository.GetTodoByIdAsync(request.Id, token);
        BusinessException.ThrowIfNull(result, "Attribute option was not found");
        return result;
    }
}
