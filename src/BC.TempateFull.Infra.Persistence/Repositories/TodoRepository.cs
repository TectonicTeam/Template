using Dekacore.Persistence.EntityFramework;
using Ds.FleetManagement.Domain.ToDo;
using BC.TempateFull.Infra.Persistence;

namespace BC.TempateFull.Infra.Persistence.Repositories;

public class TodoRepository(ServiceDbContext dbContext) : DomainRepository<Todo>(dbContext), ITodoRepository
{
}