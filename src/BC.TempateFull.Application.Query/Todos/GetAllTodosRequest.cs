using MediatR;
using Dekacore.Abstraction.Query;

namespace BC.TempateFull.Application.Query.Todos;

public record GetAllTodosRequest : GetAllTodosQuery,
    IRequest<IPagedList<GetAllTodosResult>>
{
    public long? Id { get; set; }
    public string? Title { get; set; }
    public string? Value { get; set; }
    public string? Tag { get; set; }
}

public class GetAllAttributeOptionsRequestHandler(ITodoQueryRepository repository)
    : IRequestHandler<GetAllTodosRequest, IPagedList<GetAllTodosResult>>
{
    public Task<IPagedList<GetAllTodosResult>> Handle(GetAllTodosRequest request, CancellationToken token)
    {
        return repository.GetPaginatedTodosAsync(request, token);
    }
}