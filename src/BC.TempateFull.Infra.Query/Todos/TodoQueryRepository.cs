using Dekacore.Abstraction.Query;
using Dekacore.Extension.EntityFramework;
using BC.TempateFull.Application.Query.Todos;
using BC.TempateFull.Infra.Query;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Queries.Repositories;

internal class TodoQueryRepository(QueryDbContext dbContext) : ITodoQueryRepository
{
    public Task<List<GetAllTodosResult>> GetAllTodosAsync(GetAllTodosQuery query, CancellationToken token = default)
    {
        
        return dbContext.Todos
            .Include(r => r.Items)
            .ApplyFilter(query)
            .ApplySort(query)
            .Select(x => new GetAllTodosResult(x.Id, x.Title, x.Value, x.OptionType, DateTime.Now.AddYears(-3), x.Total, x.Tag, x.Items.Select(i => i.Name).ToArray()))
            .ToListAsync(token);
    }

    public Task<IPagedList<GetAllTodosResult>> GetPaginatedTodosAsync(GetAllTodosQuery query, CancellationToken token = default)
    {
        return dbContext.Todos
           .Include(r => r.Items)
           .ApplyFilter(query)
           .ApplySort(query)
           .Select(x => new GetAllTodosResult(x.Id, x.Title, x.Value, x.OptionType, DateTime.Now.AddYears(-3), x.Total, x.Tag, x.Items.Select(i => i.Name).ToArray()))
           .ToPagedListAsync(query , token);
    }

    public Task<GetTodoByIdResult?> GetTodoByIdAsync(long id, CancellationToken token = default)
    {
        return dbContext.Todos
           .Where(r => r.Id == id)
           .Select(x => new GetTodoByIdResult(x.Id,x.Title,x.Value,x.Tag,x.Items.Select( i => i.Name).ToArray())
           )
           .SingleOrDefaultAsync(cancellationToken: token)!;

       
    }
}