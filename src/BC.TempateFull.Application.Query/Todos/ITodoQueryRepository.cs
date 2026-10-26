using Dekacore.Abstraction.Query;

namespace BC.TempateFull.Application.Query.Todos;

public interface ITodoQueryRepository : IQueryRepository
{
    Task<IPagedList<GetAllTodosResult>> GetPaginatedTodosAsync(GetAllTodosQuery query, CancellationToken token = default);
    Task<List<GetAllTodosResult>> GetAllTodosAsync(GetAllTodosQuery query, CancellationToken token = default);
    Task<GetTodoByIdResult?> GetTodoByIdAsync(long id, CancellationToken token = default);
}
